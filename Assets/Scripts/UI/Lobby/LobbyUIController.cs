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

    [Header("Panels")]
    [SerializeField] private RectTransform _soundMenu;
    [SerializeField] private RectTransform _menu;
    [SerializeField] private RectTransform _selectMode;

    [Header("Event")]
    [SerializeField] private UnityEvent LoadEasySceneEvent;
    [SerializeField] private UnityEvent LoadHardSceneEvent;

    [Header("Move")]
    public float _duration = 1;
    private Ease _moveEase = Ease.OutQuart;


    private void OnEnable()
    {
        //main menu button
        _playBtn.onClick.AddListener(() => ChangeComponentPos(_menu, new Vector2(0, -1100), _duration, _moveEase));
        _playBtn.onClick.AddListener(() => ChangeComponentPos(_selectMode, new Vector2(0, -400), _duration, _moveEase));
        _settingBtn.onClick.AddListener(() => ChangeComponentPos(_menu, new Vector2(0, 1100), _duration, _moveEase));
        _settingBtn.onClick.AddListener(() => ChangeComponentPos(_soundMenu, new Vector2(0, 550), _duration, _moveEase));
        //select mode button
        _easyModeBtn.onClick.AddListener(() => SelectMode(1));
        _hardModeBtn.onClick.AddListener(() => SelectMode(2));
        _backSelectBtn.onClick.AddListener(() => ChangeComponentPos(_selectMode, new Vector2(0, 796), _duration, _moveEase));
        _backSelectBtn.onClick.AddListener(() => ChangeComponentPos(_menu, Vector2.zero, _duration, _moveEase));
        //sound menu button
        _backSoundBtn.onClick.AddListener(() => ChangeComponentPos(_soundMenu, new Vector2(0, -400), _duration, _moveEase));
        _backSoundBtn.onClick.AddListener(() => ChangeComponentPos(_menu, Vector2.zero, _duration, _moveEase));
    }

    private void OnDisable()
    {
        _playBtn.onClick.RemoveAllListeners();
        _settingBtn.onClick.RemoveAllListeners();
        _easyModeBtn.onClick.RemoveAllListeners();
        _hardModeBtn.onClick.RemoveAllListeners();
        _backSelectBtn.onClick.RemoveAllListeners();
        _backSoundBtn.onClick.RemoveAllListeners();
    }


    public void ChangeComponentPos(RectTransform currComponent, Vector2 endPos, float duration, Ease moveEase)
    {
        currComponent.DOAnchorPos(endPos, duration).SetEase(moveEase).SetLink(gameObject);
    }


    private void SelectMode(int i)
    {
        if (i == 1)
        {
            LoadEasySceneEvent?.Invoke();
        }
        else if (i == 2)
        {
            LoadHardSceneEvent?.Invoke();
        }
    }




}
