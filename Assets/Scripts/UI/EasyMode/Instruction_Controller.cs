using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Instruction_Controller : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private GameObject _blocker;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private UnityEvent OnExitTurtorial;

    private Tween _panelTween;
    private DataManagerService _dataManager;

    private void Awake()
    {
        // Gắn sự kiện cho nút Exit
        _exitBtn.onClick.AddListener(CloseInstruction);
        _dataManager = FindAnyObjectByType<DataManagerService>();


    }

    private void OnEnable()
    {
        if (_dataManager != null)
        {
            if (!_dataManager.HasSeenTutorial())
            {
                OpenInstruction();
            }
            else
            {
                CloseInstruction();
            }
        }


    }

    public void OpenInstruction()
    {
        Time.timeScale = 0;
        _blocker.SetActive(true);
        _exitBtn.interactable = false;

        // Hủy tween cũ nếu có để tránh lỗi đè tween
        _panelTween?.Kill();

        // Kéo panel về Vector3.zero (Local Move)

        _panelTween = _panel.DOLocalMove(Vector3.zero, 1.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                _exitBtn.interactable = true;
            }); // SetUpdate(true) để animation chạy mượt ngay cả khi Time.timeScale = 0
    }

    // Hàm này sẽ được gọi khi bấm nút Exit
    public void CloseInstruction()
    {
        _exitBtn.interactable = false;
        _panelTween?.Kill();
        // Kéo panel về AnchorPos (0, -500)
        _panelTween = _panel.DOAnchorPos(new Vector2(0, -500), 0.8f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                // Tắt Blocker và tắt luôn cả cụm Instruction sau khi panel chạy xuống xong
                if (_blocker != null) _blocker.SetActive(false);

                // Trả lại thời gian thực cho Game và Lưu dữ liệu đã xem Tutorial

                Time.timeScale = 1f;
                if (_dataManager != null) _dataManager.SetHasSeenTutorial(true);
                OnExitTurtorial?.Invoke();
                gameObject.SetActive(false);
            });
    }

    private void OnDisable()
    {
        // Luôn phải dọn dẹp (Kill) tween khi object bị disable đột ngột để tránh memory leak và lỗi
        _panelTween?.Kill();

        // Reset vị trí và tắt blocker để chuẩn bị sạch sẽ cho lần bật tiếp theo
        if (_panel != null) _panel.anchoredPosition = new Vector2(0, -500f);
        if (_blocker != null) _blocker.SetActive(false);
    }
}
