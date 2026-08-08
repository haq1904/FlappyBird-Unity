using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game_Over_Panel : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent OnRestart;
    [SerializeField] UnityEvent OnBackHome;

    [Header("Game objects")]
    [SerializeField] private GameObject blocker;
    [SerializeField] private RectTransform gameOverMenu;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button restart;
    [SerializeField] private Button backHome;

    private void Start()
    {
        restart.onClick.AddListener(Restart);
        backHome.onClick.AddListener(BackHome);
    }

    public void Restart()
    {
        Sequence s = DOTween.Sequence();
        s.SetUpdate(true);
        s.AppendInterval(0.15f);
        s.AppendCallback(() =>
        {
            TurnOff();
            OnRestart?.Invoke();
        }
        );
    }

    public void BackHome()
    {
        OnBackHome?.Invoke();
    }

    public void TurnOn()
    {
        Sequence s = DOTween.Sequence();
        s.SetUpdate(true);
        s.AppendCallback(() =>
        {
            if (pausePanel.activeSelf) pausePanel.SetActive(false);
            gameObject.SetActive(true);
            blocker.SetActive(true);
        });
        s.AppendInterval(1f);
        s.AppendCallback(() =>
        {
            gameOverMenu.DOLocalMove(Vector2.zero, 1f).SetEase(Ease.OutCirc).SetLink(gameObject).SetUpdate(true);
            ButtonInteractable(true);
        });
    }

    public void TurnOff()
    {
        gameOverMenu.anchoredPosition = new Vector3(0, -500, 0);
        ButtonInteractable(false);
        gameObject.SetActive(false);
    }

    private void ButtonInteractable(bool isInteractable)
    {
        restart.interactable = isInteractable;
        backHome.interactable = isInteractable;
    }
}
