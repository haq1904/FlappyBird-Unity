using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Pause_Panel : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent OnResume;
    [SerializeField] UnityEvent OnRestart;
    [SerializeField] UnityEvent OnBackHome;

    [SerializeField] private GameObject blocker;
    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button backHome;

    private void Start()
    {
        resume.onClick.AddListener(Resume);
        restart.onClick.AddListener(Restart);
        backHome.onClick.AddListener(BackHome);
    }

    public void Resume()
    {
        Sequence s = DOTween.Sequence();
        s.SetUpdate(true);
        s.AppendInterval(0.15f);
        s.AppendCallback(() => {
            TurnOff();
            OnResume?.Invoke();
        }
        );
        
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
        gameObject.SetActive(true);
        pauseMenu.DOAnchorPos(new Vector2(0,500), 1f).SetEase(Ease.OutCirc).SetLink(gameObject).SetUpdate(true);
        blocker.SetActive(true);
    }

    public void TurnOff()
    {
        pauseMenu.anchoredPosition = new Vector3(0, -500, 0);
        gameObject.SetActive(false);
    }
}
