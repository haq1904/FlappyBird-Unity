using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    }

    private void OnEnable()
    {
        resume.onClick.AddListener(Resume);
        restart.onClick.AddListener(Restart);
        backHome.onClick.AddListener(BackHome);
    }

    private void OnDisable()
    {
        resume.onClick.RemoveAllListeners();
        restart.onClick.RemoveAllListeners();
        backHome.onClick.RemoveAllListeners();
    }

    public void Resume()
    {
        Sequence s = DOTween.Sequence();
        s.SetUpdate(true);
        s.AppendInterval(0.15f);
        s.AppendCallback(() =>
        {
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
        ButtonInteractable(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        pauseMenu.DOAnchorPos(new Vector2(0, 500), 1f).SetEase(Ease.OutCirc).SetLink(gameObject).SetUpdate(true);
        blocker.SetActive(true);
        ButtonInteractable(true);
    }

    public void TurnOff()
    {
        pauseMenu.anchoredPosition = new Vector3(0, -500, 0);
        ButtonInteractable(false);
        gameObject.SetActive(false);
    }

    private void ButtonInteractable(bool isInteractable)
    {
        resume.interactable = isInteractable;
        restart.interactable = isInteractable;
        backHome.interactable = isInteractable;
    }
}
