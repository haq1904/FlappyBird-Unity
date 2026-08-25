using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class Coin : ItemService
{
    [Header("Fields")]
    [SerializeField] private float _force;
    [SerializeField] private float _timeToFade = 1;
    [SerializeField] private Ease _easeToFade = Ease.OutElastic;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _angularVelocity = 10;

    private Rigidbody2D _rb;
    private bool _isTaken = false;
    private bool _isGameOver = false;
    private ObjectPoolingService _poolService;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _poolService = FindAnyObjectByType<ObjectPoolingService>();
    }

    private void OnEnable()
    {
        _isTaken = false;
        _isGameOver = false;
        if (_rb != null)
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
        transform.rotation = Quaternion.identity;
        if (spr != null)
        {
            spr.color = Color.white;
            spr.DOKill();
        }

        _animator.Play("Idle", -1, Random.Range(0f, 1f));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IReceivable>(out var gameObj) && !_isTaken && !_isGameOver)
        {
            Sequence s = DOTween.Sequence();
            s.SetLink(gameObject);
            s.AppendCallback(() =>
            {
                ApplyEffect(gameObj);
                float finalX = UnityEngine.Random.Range(-1f, 0.25f);
                _rb.linearVelocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Dynamic;
                _rb.AddForce(new Vector2(finalX, 1) * _force, ForceMode2D.Impulse);
                _rb.angularVelocity = Random.Range(-_angularVelocity, _angularVelocity);
                _isTaken = true;
            });
            s.Append(spr.DOFade(0, _timeToFade).SetEase(_easeToFade));
            s.AppendCallback(() =>
            {
                if (gameObject.activeSelf)
                {
                    if (_poolService != null) _poolService.ReturnObjectToPool(gameObject);
                }
            });
        }
        else if (collision.CompareTag("PoolCollector") && gameObject.activeSelf)
        {
            if (_poolService != null) _poolService.ReturnObjectToPool(gameObject);
        }
    }


    private void FixedUpdate()
    {
        if (!_isTaken)
        {
            _rb.linearVelocity = Vector2.left * _speed;
        }
    }

    public override void ApplyEffect(IReceivable gameObj)
    {
        _effect.ApplyEffect(gameObj);
    }

    public override void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void HandleGameOver()
    {
        _speed = 0;
        _rb.linearVelocity = Vector2.zero;
        _isGameOver = true;
    }

    public void HandleRestart()
    {
        if (!gameObject.activeSelf) return;

        if (_poolService != null)
            _poolService.ReturnObjectToPool(gameObject);
    }


}
