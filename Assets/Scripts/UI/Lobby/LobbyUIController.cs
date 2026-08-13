using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LobbyUIController : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _settingBtn;
    [SerializeField] private Button _backSoundBtn;
    [SerializeField] private Button _customBtn;
    [SerializeField] private Button _backCustomBtn;

    [Header("Panels")]
    [SerializeField] private RectTransform _soundMenu;
    [SerializeField] private RectTransform _menu;
    [SerializeField] private RectTransform _custom;
    [SerializeField] private CanvasGroup _customCanvasGroup;

    [Header("Event")]
    [SerializeField] private UnityEvent LoadEasySceneEvent;
    [SerializeField] private UnityEvent LoadHardSceneEvent;
    [SerializeField] private SoundTypeGameEvent _soundTypeGameEvent;


    [Header("Move")]
    public float _duration = 1;
    private Ease _moveEase = Ease.OutQuart;

    private Button[] _allButton;


    private void Awake()
    {
        ButtonSubscribeListener();
        _allButton = GetComponentsInChildren<Button>(true);
        foreach (Button btn in _allButton)
        {
            btn.onClick.AddListener(() => PlaySound(SoundType.Click));
        }
    }

    private void OnEnable()
    {
        ButtonInteractable(true);

    }

    private void ButtonSubscribeListener()
    {
        //menu button
        _playBtn.onClick.AddListener(() => PlayGame());
        _settingBtn.onClick.AddListener(() => ChangeComponentPos(_menu, new Vector2(0, 1100), _duration, _moveEase));
        _settingBtn.onClick.AddListener(() => MoveToCenter(_soundMenu, _duration, _moveEase));
        _customBtn.onClick.AddListener(() =>
        {
            _custom.gameObject.SetActive(true);
            ChangeComponentPos(_custom, Vector2.zero, _duration, _moveEase);
            DoFadeCanvasGroup(_customCanvasGroup, 1, _duration, _moveEase, true);
        });
        //sound menu button
        _backSoundBtn.onClick.AddListener(() => ChangeComponentPos(_soundMenu, new Vector2(0, -400), _duration, _moveEase));
        _backSoundBtn.onClick.AddListener(() => MoveToCenter(_menu, _duration, _moveEase));
        //custom button
        _backCustomBtn.onClick.AddListener(() =>
        {
            ChangeComponentPos(_custom, new Vector2(0, 100), _duration, _moveEase);
            DoFadeCanvasGroup(_customCanvasGroup, 0, _duration, _moveEase, false);
        });
    }

    private void ChangeComponentPos(RectTransform currComponent, Vector2 endPos, float duration, Ease moveEase)
    {
        currComponent.DOAnchorPos(endPos, duration).SetEase(moveEase).SetLink(gameObject);
    }

    private void MoveToCenter(RectTransform currComponent, float duration, Ease moveEase)
    {
        currComponent.DOLocalMove(Vector2.zero, duration).SetEase(moveEase).SetLink(gameObject);
    }


    private void DoFadeCanvasGroup(CanvasGroup componentCavasGroup, float endValue, float duration, Ease moveEase, bool isEnable)
    {
        componentCavasGroup.DOFade(endValue, duration)
        .SetEase(moveEase)
        .SetLink(gameObject)
        .OnComplete(() => { componentCavasGroup.gameObject.SetActive(isEnable); });

    }

    private void PlaySound(SoundType soundType)
    {
        _soundTypeGameEvent?.Raise(soundType);
    }

    private void PlayGame()
    {
        SceneController.Instance.LoadScene(2);
        ButtonInteractable(false);
    }

    private void ButtonInteractable(bool isInteractable)
    {
        foreach (Button btn in _allButton)
        {
            btn.interactable = isInteractable;
        }
    }



}
