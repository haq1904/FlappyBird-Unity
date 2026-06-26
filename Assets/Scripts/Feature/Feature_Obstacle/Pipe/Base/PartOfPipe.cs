using DG.Tweening;
using UnityEngine;

public class PartOfPipe: MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private GameObject _smallCrack;
    [SerializeField] private float _impactForce = 1;
    [SerializeField] private float _gravityScale = 1;

    [Header("Punch")]
    [SerializeField] private float _duration = 1;
    [SerializeField] private Vector3 _streng;
    [SerializeField] private int _vibrato = 10;
    [SerializeField,Range(0,1)] private float _elasticity  = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageableGameObject))
        {
            damageableGameObject.TakeDamage(Vector2.left, _impactForce, _gravityScale);
            transform.DOPunchPosition(_streng, _duration, _vibrato, _elasticity).SetLink(gameObject);
            Vector2 directionOfCollision = collision.GetContact(0).normal;
            if (directionOfCollision.x > 0.5f) //This condition force cracks to be spawned at the left side of catus
            {
                Vector2 collisionWorldPos = collision.GetContact(0).point;
                Vector2 collisionLocalPos = transform.InverseTransformPoint(collisionWorldPos);
                _smallCrack.SetActive(true);
                _smallCrack.transform.localPosition = collisionLocalPos;
            }
        }
        
    }

    private void OnDisable()
    {
        _smallCrack.SetActive(false);
    }
}
