using UnityEngine;

public class PartOfPipe: MonoBehaviour
{
    [SerializeField] float impactForce = 1;
    [SerializeField] float gravityScale = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageableGameObject)) {
            damageableGameObject.TakeDamage(Vector2.left, impactForce, gravityScale);
        }
    }
}
