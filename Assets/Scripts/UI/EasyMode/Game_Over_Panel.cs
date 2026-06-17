using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Game_Over_Panel : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent OnRestart;
    [SerializeField] UnityEvent OnBackHome;

    [Header("Game objects")]
    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject gameOverMenu;
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
        s.AppendCallback(() => {
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
            gameOverMenu.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutCirc).SetLink(gameObject).SetUpdate(true);
        });
    }

    public void TurnOff()
    {
        gameOverMenu.transform.localPosition = new Vector3(0, -1100, 0);
        gameObject.SetActive(false);
    }
}
