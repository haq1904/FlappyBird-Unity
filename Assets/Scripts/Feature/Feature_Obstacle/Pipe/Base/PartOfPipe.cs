using DG.Tweening;
using UnityEngine;

public class PartOfPipe: MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private GameObject _smallCrack;
    [SerializeField] private float _impactForce = 1;
    [SerializeField] private float _gravityScale = 1;

    [Header("Shake")]
    [SerializeField] private float _duration = 1;
    [SerializeField] private Vector3 _streng;
    [SerializeField] private int _vibrato = 10;
    [SerializeField,Range(0,180)] private float _randomness = 90;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageableGameObject))
        {
            damageableGameObject.TakeDamage(Vector2.left, _impactForce, _gravityScale);
            transform.DOShakePosition(_duration, _streng, _vibrato, _randomness).SetLink(gameObject);
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
