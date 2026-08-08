using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LobbyUIController : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _settingBtn;
    [SerializeField] private Button _easyModeBtn;
    [SerializeField] private Button _hardModeBtn;
    [SerializeField] private Button _backSelectBtn;
    [SerializeField] private Button _backSoundBtn;
    [SerializeField] private Button _customBtn;
    [SerializeField] private Button _backCustomBtn;

    [Header("Panels")]
    [SerializeField] private RectTransform _soundMenu;
    [SerializeField] private RectTransform _menu;
    [SerializeField] private RectTransform _selectMode;
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


    private void Start()
    {
        ButtonSubscribeListener();
        _easyModeBtn.interactable = true;
        _hardModeBtn.interactable = true;
        _allButton = GetComponentsInChildren<Button>(true);
        foreach (Button btn in _allButton)
        {
            btn.onClick.AddListener(() => PlaySound(SoundType.Click));
        }
    }

    private void ButtonSubscribeListener()
    {
        //menu button
        _playBtn.onClick.AddListener(() => ChangeComponentPos(_menu, new Vector2(0, -1100), _duration, _moveEase));
        _playBtn.onClick.AddListener(() => MoveToCenter(_selectMode, _duration, _moveEase));
        _settingBtn.onClick.AddListener(() => ChangeComponentPos(_menu, new Vector2(0, 1100), _duration, _moveEase));
        _settingBtn.onClick.AddListener(() => MoveToCenter(_soundMenu, _duration, _moveEase));
        _customBtn.onClick.AddListener(() =>
        {
            _custom.gameObject.SetActive(true);
            ChangeComponentPos(_custom, Vector2.zero, _duration, _moveEase);
            DoFadeCanvasGroup(_customCanvasGroup, 1, _duration, _moveEase, true);
        });
        //select mode button
        _easyModeBtn.onClick.AddListener(() => SelectMode(1));
        _hardModeBtn.onClick.AddListener(() => SelectMode(2));
        _backSelectBtn.onClick.AddListener(() => ChangeComponentPos(_selectMode, new Vector2(0, 250), _duration, _moveEase));
        _backSelectBtn.onClick.AddListener(() => MoveToCenter(_menu, _duration, _moveEase));
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


    private void SelectMode(int i)
    {
        _easyModeBtn.interactable = false;
        _hardModeBtn.interactable = false;
        if (i == 1)
        {
            LoadEasySceneEvent?.Invoke();
        }
        else if (i == 2)
        {
            LoadHardSceneEvent?.Invoke();
        }
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




}
