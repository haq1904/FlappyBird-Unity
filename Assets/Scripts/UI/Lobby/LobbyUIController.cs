using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Xml.Serialization;
public class LobbyUIController : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button easyModeBtn;
    [SerializeField] private Button hardModeBtn;
    [SerializeField] private Button backToLobbyBtn;
    [SerializeField] private Button backSoundBtn;

    [Header("Panels")]
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private RectTransform menu;
    [SerializeField] private RectTransform selectMode;

    
    
    private bool isTurnOnSoundMenu = false;
    public float duration = 1;
    private Ease moveEase = Ease.OutQuart;

    public static Action OnPlayEasyMode;

    public static Action OnPlayHardMode;

    private void OnEnable(){
        playBtn.onClick.AddListener(OpenSelectMode);
        soundBtn.onClick.AddListener(TurnOnSoundMenu);
        easyModeBtn.onClick.AddListener(()=>SelectMode(0));
        hardModeBtn.onClick.AddListener(()=>SelectMode(1));
        backToLobbyBtn.onClick.AddListener(BackToMenu);
        backSoundBtn.onClick.AddListener(TurnOnSoundMenu);
    }

    private void OnDisable(){
        playBtn.onClick.RemoveAllListeners();
        soundBtn.onClick.RemoveAllListeners();
        easyModeBtn.onClick.RemoveAllListeners();
        hardModeBtn.onClick.RemoveAllListeners();
        backToLobbyBtn.onClick.RemoveAllListeners();
        OnPlayEasyMode=null;
        OnPlayHardMode=null;
    }

    

    public void TurnOnSoundMenu()
    {
        if (!isTurnOnSoundMenu)
        {
            soundMenu.SetActive(true);
            isTurnOnSoundMenu = true;
        }
        else
        {
            soundMenu.SetActive(false);
            isTurnOnSoundMenu = false;
        }
    }

    public void OpenSelectMode()
    {
        menu.DOAnchorPos(new Vector2(0,-1100), duration).SetEase(moveEase).SetLink(gameObject);
        selectMode.DOAnchorPos(new Vector2(0, -395), duration).SetEase(moveEase).SetLink(gameObject);
    }

    public void BackToMenu()
    {
        selectMode.DOAnchorPos(new Vector2(0, 796), duration).SetEase(moveEase).SetLink(gameObject);
        menu.DOAnchorPos(Vector2.zero, duration).SetEase(moveEase).SetLink(gameObject);
    }

    private void SelectMode(int i){
        if(i==0){
            OnPlayEasyMode?.Invoke();
        }
        else if(i==1){
            OnPlayHardMode?.Invoke();
        }
    }

    


}
