using DG.Tweening;
using UnityEngine;

public class Storm : ObstacleService
{
    [Header("Fields")]
    [SerializeField] private Transform _visual;
    [SerializeField] private float _rotateDuration;

    [Header("Shake fields")]
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private Vector3 _shakeStrength = new Vector3(0.2f, 0.2f, 0);
    [SerializeField] private int _shakeVibrato = 10;
    [SerializeField] private float _shakeRandomness = 90f;

    private Rigidbody2D _rb;
    private float _moveSpeed = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
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
        _moveSpeed = 0;
    }

    public override void SetSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }
}
