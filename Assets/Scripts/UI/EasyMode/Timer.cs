using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnTimerDoneEvent;

    [Header("Game objects")]
    [SerializeField] private RectTransform _panel;
    [SerializeField] private GameObject[] _birds;

    [Header("Fields")]
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _timeToCountdown = 1;
    [SerializeField] private GameObject _botMarker;

    [Header("Shake")]
    [SerializeField] float _durationForShake = 1;
    [SerializeField] Vector2 _streght = new Vector2(5, 5);
    [SerializeField] int _vibrato = 10;
    [SerializeField] int _randomness = 90;

    [Header("Jump fields")]
    [SerializeField] private int _numJump;
    [SerializeField] private float _jumpDuration;


    private Ease _moveEase = Ease.InOutBack;
    private List<Vector3> _resetBirdsPos;
    private Sequence _mainSequence;
    private Vector3 _panelResetPos;

    private void Awake()
    {
        _resetBirdsPos = new List<Vector3>();
        _panelResetPos = _panel.anchoredPosition;
        for (int i = 0; i < _birds.Length; i++)
        {
            _resetBirdsPos.Add(_birds[i].GetComponent<RectTransform>().anchoredPosition);
        }

    }

    private void OnEnable()
    {
        Play();
    }

    private void OnDisable()
    {
        _mainSequence.Kill();
        HandleResetBird();
        _panel.anchoredPosition = _panelResetPos;

    }

    private void Play()
    {
        _mainSequence = DOTween.Sequence();
        _mainSequence.Append(_panel.DOAnchorPosY(-200f, _duration).SetEase(_moveEase));

        float jumpTime = _duration;
        for (int i = 0; i < _birds.Count(); i++)
        {
            _birds[i].SetActive(true);
            RectTransform birdRect = _birds[i].GetComponent<RectTransform>();
            float randRotateZValue = Random.value > 0.5f ? Random.Range(-1080, 360) : Random.Range(360, 1080);
            Vector2 randEndValue = new Vector2(Random.Range(-3f, 3f), _botMarker.transform.position.y);
            float randJumPower = Random.Range(9f, 11f);
            _mainSequence.Insert(jumpTime, birdRect.DOLocalRotate(new Vector3(0, 0, randRotateZValue), _jumpDuration, RotateMode.FastBeyond360).SetEase(Ease.InQuad));
            _mainSequence.Insert(jumpTime, birdRect.DOJump(randEndValue, randJumPower, 1, _jumpDuration));
            _mainSequence.Insert(jumpTime, _panel.DOShakeAnchorPos(_durationForShake, _streght, _vibrato, _randomness));
            jumpTime += _timeToCountdown;
            if (i == (_birds.Count() - 1))
            {
                _mainSequence.InsertCallback(jumpTime, () =>
                {
                    OnTimerDoneEvent?.Invoke();
                });
            }

        }
        _mainSequence.AppendCallback(() =>
        {
            HandleResetBird();
        });
        _mainSequence.Append(_panel.DOAnchorPos(_panelResetPos, _duration).SetEase(_moveEase));

    }

    private void HandleResetBird()
    {
        for (int i = 0; i < _birds.Count(); i++)
        {
            _birds[i].SetActive(false);
            RectTransform birdRect = _birds[i].GetComponent<RectTransform>();
            birdRect.anchoredPosition = _resetBirdsPos[i];
            birdRect.rotation = Quaternion.identity;
        }
    }
}
