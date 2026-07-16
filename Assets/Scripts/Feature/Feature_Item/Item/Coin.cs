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
    private bool _IsTaken = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _animator.Play("Idle", -1, Random.Range(0f, 1f));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IReceivable>(out var gameObj) && !_IsTaken)
        {
            Sequence s = DOTween.Sequence();
            s.AppendCallback(() =>
            {
                ApplyEffect(gameObj);
                float finalX = UnityEngine.Random.Range(-1f, 0.25f);
                _rb.linearVelocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Dynamic;
                _rb.AddForce(new Vector2(finalX, 1) * _force, ForceMode2D.Impulse);
                _rb.angularVelocity = Random.Range(-_angularVelocity, _angularVelocity);
                _IsTaken = true;
            });
            s.Append(spr.DOFade(0, _timeToFade).SetEase(_easeToFade).SetLink(gameObject));
            s.AppendCallback(() => Destroy(gameObject));
        }
        else if (collision.CompareTag("PoolCollector"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        spr.DOKill();
    }


    private void FixedUpdate()
    {
        if (!_IsTaken)
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
    }

    public void HandleRestart()
    {
        Destroy(gameObject);
    }


}
