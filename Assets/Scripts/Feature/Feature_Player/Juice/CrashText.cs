using DG.Tweening;
using TMPro;
using UnityEngine;

public class CrashText : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshPro _textMesh;

    [Header("Movement Settings")]
    [SerializeField] private float _moveY = 2f;
    [SerializeField] private float _moveDuration = 0.5f;
    [SerializeField] private Ease _moveEase = Ease.OutQuart;

    [Header("Rotation Settings")]
    [SerializeField] private float _minRotation = -15f;
    [SerializeField] private float _maxRotation = 15f;

    [Header("Fade Settings")]
    [SerializeField] private float _stayDuration = 0.2f;
    [SerializeField] private float _fadeOutDuration = 0.3f;

    private Sequence _sequence;

    private void OnEnable()
    {
        if (_textMesh == null) return;

        // Reset màu về trong suốt (alpha = 0)
        Color c = _textMesh.color;
        c.a = 0f;
        _textMesh.color = c;

        // Random góc xoay
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(_minRotation, _maxRotation));

        // Tạo Sequence quản lý toàn bộ quá trình
        _sequence = DOTween.Sequence();
        _sequence.SetLink(gameObject); // Tự hủy sequence nếu object bị Destroy đột ngột

        // Giai đoạn 1: Trượt lên trên và Fade in (hiện rõ dần)
        _sequence.Append(transform.DOMoveY(transform.position.y + _moveY, _moveDuration).SetEase(_moveEase));
        _sequence.Join(DOTween.ToAlpha(() => _textMesh.color, x => _textMesh.color = x, 1f, _moveDuration));

        // Giai đoạn 2: Giữ nguyên trên đỉnh một lúc (stay)
        _sequence.AppendInterval(_stayDuration);

        // Giai đoạn 3: Fade out biến mất
        _sequence.Append(DOTween.ToAlpha(() => _textMesh.color, x => _textMesh.color = x, 0f, _fadeOutDuration));

        // Giai đoạn 4: Hủy luôn GameObject sau khi chạy xong
        _sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDisable()
    {
        // Dọn dẹp cẩn thận Tween khi object bị vô hiệu hóa
        _sequence?.Kill();
        transform.DOKill();
        if (_textMesh != null)
        {
            _textMesh.DOKill();
        }
    }

}
