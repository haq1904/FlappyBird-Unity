using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Punch fields for menu")]
    [SerializeField] private float _menuPunchDuration = 1;
    [SerializeField] private Vector3 _menuPunchStrength;
    [SerializeField] private int _menuPunchVibrato = 10;


    [Header("Fields")]
    [SerializeField] private Animator _animator;
    [SerializeField] private RectTransform _menu;

    [Header("Chat fields")]
    [SerializeField] private Image _chatPanel;
    [SerializeField] private float _panelMovetime = 1;
    [SerializeField] private Ease _panelMoveEase = Ease.OutCubic;
    [SerializeField] private TextMeshProUGUI _chatText;
    [SerializeField] private float _chatDuration = 2;
    [SerializeField] private List<string> _chatList;


    public void HandleCollision()
    {
        _animator.enabled = false;
        _menu.DOPunchPosition(_menuPunchStrength, _menuPunchDuration, _menuPunchVibrato).SetLink(gameObject);
        transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness).SetLink(gameObject).OnComplete(() =>
        {
            transform.DOLocalMoveY(-2300, _localMoveYDuration).SetEase(_localMoveYEase).SetLink(gameObject)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(15f, () =>
                {
                    _animator.enabled = true;
                    _animator.Play("Perch", -1, 0f);
                }).SetLink(gameObject);
            });
        });
    }

    public void HandleTalking()
    {
        if (_chatList != null && _chatList.Count > 0)
        {
            string currMessage = _chatList[Random.Range(0, _chatList.Count)];
            _chatText.text = currMessage;
            _chatText.maxVisibleCharacters = 0;
            _chatText.DOFade(1f, _panelMovetime).SetLink(gameObject);
            _chatPanel.rectTransform.DOLocalMoveY(0, _panelMovetime).SetEase(_panelMoveEase).SetLink(gameObject);
            _chatPanel.DOFade(1f, _panelMovetime).SetLink(gameObject).OnComplete(() =>
            {
                DOTween.To(
                    () => _chatText.maxVisibleCharacters, // Lấy giá trị hiện tại
                    x => _chatText.maxVisibleCharacters = x, // Cập nhật giá trị
                    currMessage.Length, // Giá trị đích (Hiển thị toàn bộ chữ)
                    _chatDuration // Thời gian chạy
                )
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(4f, () =>
                {
                    _chatPanel.rectTransform.DOLocalMoveY(-100, _panelMovetime).SetEase(_panelMoveEase).SetLink(gameObject);
                    _chatPanel.DOFade(0f, _panelMovetime).SetLink(gameObject);
                    _chatText.DOFade(0f, _panelMovetime).SetLink(gameObject);
                }).SetLink(gameObject);
                });


            });
        }


    }
}
