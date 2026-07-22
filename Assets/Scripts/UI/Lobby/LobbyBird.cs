using DG.Tweening;
using UnityEngine;

public class LobbyBird : MonoBehaviour
{
    [Header("LocalMoveY fiels")]
    [SerializeField] private float _localMoveYDuration = 1;
    [SerializeField] private Ease _localMoveYEase = Ease.InQuart;

    [Header("ShakePosition fields")]
    [SerializeField] private float _shakeDuration = 1;
    [SerializeField] private Vector3 _shakeStrength;
    [SerializeField] private int _shakeVibrato = 10;
    [SerializeField] private float _shakeRandomness = 90;


    [Header("Fields")]
    [SerializeField] private Animator _animator;
    [SerializeField] private RectTransform _menu;

    public void HandleCollision()
    {
        _animator.enabled = false;
        _menu.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness).SetLink(gameObject);
        transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness).SetLink(gameObject).OnComplete(() =>
        {
            transform.DOLocalMoveY(-2300, _localMoveYDuration).SetEase(_localMoveYEase).SetLink(gameObject);
        });
    }
}
