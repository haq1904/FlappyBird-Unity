using System.Xml.Serialization;
using DG.Tweening;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private float _followSpeed = 1;
    [SerializeField] private float _minDurationToFollow = 2f;
    [SerializeField] private float _maxDurationToFollow = 4f;
    [SerializeField] private float _timeToDisapear = 1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Vector3 _shakeAngle;
    [SerializeField] private int _vibrato = 10;
    [SerializeField, Range(0, 180)] private float _randomness = 90;


    public Vector2 LastPosition { get; private set; }
    public float DurationToFollow { get; private set; } = 0;

    private PlayerService _targetGameObj;
    private Sequence _mainSequence;
    private ObjectPoolingService _poolService;

    private void Awake()
    {
        _poolService = FindAnyObjectByType<ObjectPoolingService>();
    }

    private void OnEnable()
    {
        DurationToFollow = UnityEngine.Random.Range(_minDurationToFollow, _maxDurationToFollow);
        _animator.Play("PlayWarning");

        // Cần xài DOKill ở OnDisable vì SetLink không tự hủy khi OnDisable (nó chỉ hủy khi OnDestroy)
        transform.DOShakeRotation(DurationToFollow + _timeToDisapear, _shakeAngle, _vibrato, _randomness, false).SetLink(gameObject);

        _mainSequence = DOTween.Sequence();
        _mainSequence.SetLink(gameObject);
        _mainSequence.AppendInterval(DurationToFollow);
        _mainSequence.AppendCallback(() =>
        {
            LastPosition = transform.position;
            _followSpeed = 0;
        });
        _mainSequence.AppendInterval(_timeToDisapear);
        _mainSequence.AppendCallback(() => ReturnToPool());
    }

    public void ReturnToPool()
    {
        if (gameObject.activeSelf && _poolService != null)
        {
            _poolService.ReturnObjectToPool(gameObject);
        }
    }

    private void OnDisable()
    {
        _mainSequence?.Kill();
        transform.DOKill();
    }

    void Update()
    {
        if (_targetGameObj != null)
        {
            float newY = Mathf.Lerp(transform.position.y, _targetGameObj.GetTransform().position.y, _followSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, newY);
        }
    }


    public void SetTarget(PlayerService target)
    {
        _targetGameObj = target;
    }

    public void SetFollowSpeed(float moveSpeed)
    {
        _followSpeed = moveSpeed;
    }

    public void HandleRestart()
    {
        ReturnToPool();
    }

    public void HandleGameOver()
    {
        _followSpeed = 0;
    }
}
