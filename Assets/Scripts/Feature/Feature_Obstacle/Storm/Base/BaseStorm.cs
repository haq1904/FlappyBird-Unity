using DG.Tweening;
using UnityEngine;

public class BaseStorm : ObstacleService
{
    [Header("Fields")]
    [SerializeField] private Transform _visual;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private CircleCollider2D _suctionCollider;

    [Header("Shake fields")]
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private Vector3 _shakeStrength = new Vector3(0.2f, 0.2f, 0);
    [SerializeField] private int _shakeVibrato = 10;
    [SerializeField] private float _shakeRandomness = 90f;

    protected Rigidbody2D _rb;
    protected PointEffector2D _pointEffector;
    [SerializeField] protected float _moveSpeed = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pointEffector = GetComponent<PointEffector2D>();

    }

    protected virtual void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.left * _moveSpeed;
    }

    private void OnEnable()
    {
        _visual.DORotate(new Vector3(0, 0, 360), _rotateDuration, RotateMode.FastBeyond360)
        .SetLink(gameObject)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);

        _visual.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness, false, false)
        .SetLink(gameObject)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }

    public void HandleRestart()
    {
        Destroy(gameObject);
    }

    public void HandleGameOver()
    {
        _pointEffector.linearDamping = 0;
        _moveSpeed = 0;
    }

    public override void SetSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public override void SetRadius(float value)
    {
        _suctionCollider.radius = value;
    }


    public override void SetForceMagnitude(float force)
    {
        _pointEffector.forceMagnitude = force;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PoolCollector"))
        {
            Destroy(gameObject);
        }
    }
}
