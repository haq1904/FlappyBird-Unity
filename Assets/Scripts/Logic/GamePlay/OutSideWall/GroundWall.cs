using UnityEngine;

public class GroundWall : MonoBehaviour
{
    [SerializeField] float force = 1;
    [SerializeField] float gravity = 1;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var gameObject)){
            gameObject.TakeDamage(Vector2.up, force, gravity);
        }
    }


}

