using DG.Tweening;
using UnityEngine;

public class UI_Shake : MonoBehaviour
{
    [Header("Shake Position fields")]
    [SerializeField] private float _shakePositionDuration = 1;
    [SerializeField] private Vector3 _shakePositionStrength;
    [SerializeField] private int _shakePositionVibrato = 10;
    [SerializeField] private float _shakePositionRandomness = 90;

    [Header("Shake Rotation fields")]
    [SerializeField] private float _shakeRotationDuration = 1;
    [SerializeField] private Vector3 _shakeRotationStrength;
    [SerializeField] private int _shakeRotationVibrato = 10;
    [SerializeField] private float _shakeRotationRandomness = 90;

    [SerializeField] private bool isShakePosition = false;
    [SerializeField] private bool isShakeRotation = false;
    [SerializeField] private bool isShakeOnEnable = false;
    [SerializeField] private bool isFadeOut = false;
    [SerializeField] private bool isLoop = false;

    private void OnEnable()
    {
        if (isShakeOnEnable)
            PlayShake();
    }

    private void OnDisable()
    {
        transform.DOKill(true);
    }

    public void PlayShake()
    {
        transform.DOKill(true); // Giết sạch trước khi bắt đầu

        if (isShakePosition)
            LoopShakePosition();

        if (isShakeRotation)
            LoopShakeRotation();
    }

    private void LoopShakePosition()
    {
        if (isLoop)
            transform.DOShakePosition(_shakePositionDuration, _shakePositionStrength, _shakePositionVibrato, _shakePositionRandomness, false, isFadeOut)
                     .OnComplete(() => LoopShakePosition()) // Đệ quy riêng của Position
                     .SetLink(gameObject);
        else
            transform.DOShakePosition(_shakePositionDuration, _shakePositionStrength, _shakePositionVibrato, _shakePositionRandomness, false, isFadeOut).SetLink(gameObject);
    }

    private void LoopShakeRotation()
    {
        if (isLoop)
            transform.DOShakeRotation(_shakeRotationDuration, _shakeRotationStrength, _shakeRotationVibrato, _shakeRotationRandomness, isFadeOut)
                     .OnComplete(() => LoopShakeRotation()) // Đệ quy riêng của Rotation
                     .SetLink(gameObject);
        else
            transform.DOShakeRotation(_shakeRotationDuration, _shakeRotationStrength, _shakeRotationVibrato, _shakeRotationRandomness, isFadeOut).SetLink(gameObject);
    }

}