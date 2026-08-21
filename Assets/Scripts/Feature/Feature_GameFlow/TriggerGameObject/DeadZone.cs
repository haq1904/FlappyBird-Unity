using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] Vector2 _directionImpact;
    [SerializeField] float _force = 1;
    [SerializeField] float _gravity = 1;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var gameObject))
        {
            gameObject.TakeDamage(_directionImpact, _force, _gravity);
        }
    }


}