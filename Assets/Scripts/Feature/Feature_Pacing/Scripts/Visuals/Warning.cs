using DG.Tweening;
using System.Xml.Serialization;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private float _followSpeed = 1;
    [SerializeField] private float _durationToFollow = 2f;
    [SerializeField] private float _timeToDisapear = 1f;

    public Vector2 LastPosition { get; private set; }
    public float DurationToFollow { get; private set; } = 0;

    private PlayerService _targetGameObj;
    private Sequence _mainSequence;

    private void OnEnable()
    {
        DurationToFollow = _durationToFollow;
        _mainSequence = DOTween.Sequence();
        _mainSequence.AppendInterval(_durationToFollow);
        _mainSequence.AppendCallback(() => {
            LastPosition = transform.position;
            _followSpeed = 0; 
        });
        _mainSequence.AppendInterval(_timeToDisapear);
        _mainSequence.AppendCallback(() => Destroy(gameObject));
    }

    private void OnDisable()
    {
        _mainSequence.Kill();
    }

    public void SetTarget(PlayerService target)
    {
        _targetGameObj = target;
    }

    void Update()
    {
        if (_targetGameObj != null)
        {
            float newY = Mathf.Lerp(transform.position.y, _targetGameObj.GetTransform().position.y, _followSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, newY);
        }
    }

    public void HandleRestart()
    {
        Destroy(gameObject);
    }

    public void HandleGameOver()
    {
        Destroy(gameObject);
    }
}
