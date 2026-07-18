using System.Diagnostics.Contracts;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class BaseAttackingBird : ObstacleService
{
    [Header("Fields")]
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _impactForce = 1;
    [SerializeField] private float _gravityScale = 1;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _rotationSpeed = 50f;

    [Header("Shake fields")]
    [SerializeField] private float _shakeDuration = 1;
    [SerializeField] private Vector3 _shakeStrength;
    [SerializeField] private int _shakeVibrato = 10;
    [SerializeField] private float _shakeRandomness = 90;


    [Header("Character Database")]
    [SerializeField] CharacterDataBaseService _characterDB;

    private Rigidbody2D _rb;
    private bool haveCollision = false;

    private void OnEnable()
    {
        _sprite.transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness).SetLink(gameObject);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        PlayAnimation();

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
        if (collision.gameObject.TryGetComponent<IDamageable>(out var gameObj))
        {
            haveCollision = true;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = _rotationSpeed;
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            _rb.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);
            gameObj.TakeDamage(Vector2.left, _impactForce, _gravityScale);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PoolCollector"))
        {
            Destroy(gameObject);
        }
    }

    private void PlayAnimation()
    {
        int randIdCharacter = UnityEngine.Random.Range(0, _characterDB.CharacterCount);
        _animator.runtimeAnimatorController = _characterDB.GetCharacterById(randIdCharacter).AnimController;
        if (_animator.runtimeAnimatorController == null) Debug.Log("Can not get animator controller from database");
        _animator.Play("Flying");
    }
}
