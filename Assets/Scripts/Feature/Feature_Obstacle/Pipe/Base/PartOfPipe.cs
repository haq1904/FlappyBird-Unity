using DG.Tweening;
using UnityEngine;

public class PartOfPipe: MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private float _impactForce = 1;
    [SerializeField] private float _gravityScale = 1;

    [Header("Shake")]
    [SerializeField] private float _durationForShaking = 1;
    [SerializeField] private Vector3 _strengForShaking;
    [SerializeField] private int _vibratoForShaking = 10;
    [SerializeField] private float _randomnessForShaking = 90;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageableGameObject)) {
            damageableGameObject.TakeDamage(Vector2.left, _impactForce, _gravityScale);
            transform.DOShakePosition(_durationForShaking, _strengForShaking, _vibratoForShaking, _randomnessForShaking,snapping : true,fadeOut : true);
        }
    }
}
