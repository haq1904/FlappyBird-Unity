using DG.Tweening;
using UnityEngine;

public class Coin : ItemService
{
    [Header("Fields")]
    [SerializeField] private float _force;
    [SerializeField] private float _timeToFade=1;
    [SerializeField] private Ease _easeToFade = Ease.OutElastic;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rb;
    private bool _IsTaken=false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _animator.Play("Idle", -1, Random.Range(0f,1f));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IReceivable>(out var gameObj) && !_IsTaken )
        {
            ApplyEffect(gameObj);
            float finalX = UnityEngine.Random.Range(-1f, 0.25f);
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(new Vector2(finalX, 1)*_force, ForceMode2D.Impulse);
            spr.DOFade(0, _timeToFade).SetEase(_easeToFade).SetLink(gameObject);
            _IsTaken = true;
        }
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
