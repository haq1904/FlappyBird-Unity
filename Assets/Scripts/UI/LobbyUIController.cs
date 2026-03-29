using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject birdLobby;
    [SerializeField] private RectTransform menu;
    [SerializeField] private RectTransform selectMode;
    private bool isTurnOnSoundMenu = false;
    public float duration = 1;
    private Ease moveEase = Ease.OutQuart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    

    public void TurnOnSoundMenu()
    {
        if (!isTurnOnSoundMenu)
        {
            Debug.Log("Turn on sound menu.");
            soundMenu.SetActive(true);
            isTurnOnSoundMenu = true;
        }
        else
        {
            Debug.Log("Turn off sound menu.");
            soundMenu.SetActive(false);
            isTurnOnSoundMenu = false;
        }
    }

    public void OpenSelectMode()
    {
        LogicManager.Instance.SelectMode();
        menu.DOAnchorPos(new Vector2(0,-1100), duration).SetEase(moveEase).SetLink(gameObject);
        selectMode.DOAnchorPos(new Vector2(0, -17), duration).SetEase(moveEase).SetLink(gameObject);
    }

    public void BackToMenu()
    {
        LogicManager.Instance.Lobby();
        selectMode.DOAnchorPos(new Vector2(0, 796), duration).SetEase(moveEase).SetLink(gameObject);
        menu.DOAnchorPos(Vector2.zero, duration).SetEase(moveEase).SetLink(gameObject);
    }

    


}
