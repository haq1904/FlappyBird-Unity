using System.Diagnostics.Contracts;
using UnityEngine;

public class BaseAttackingBird : ObstacleService
{
    [Header("Fields")]
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _impactForce = 1;
    [SerializeField] private float _gravityScale = 1;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _rotationSpeed = 50f;



    private Rigidbody2D _rb;
    private bool haveCollision = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (!haveCollision)
        {
            _rb.linearVelocity = Vector2.left * _moveSpeed;
        }
    }


    public void HandleRestart()
    {
        Destroy(gameObject);
    }

    public void HandleGameOver()
    {
        if (!haveCollision)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

    #region Override ObstacleService
    public override void SetSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public override float GetSpawnHeight()
    {
        return transform.position.y;
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamageable>(out var gameObj)){
            haveCollision = true;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = _rotationSpeed;
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            _rb.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);
            gameObj.TakeDamage(Vector2.left,_impactForce,_gravityScale);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PoolCollector"))
        {
            Destroy(gameObject);
        }
    }
}
