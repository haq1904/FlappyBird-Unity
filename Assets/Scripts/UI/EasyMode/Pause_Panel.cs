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
    [SerializeField] private GameObject pauseMenu;
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
        pauseMenu.transform.localPosition = new Vector3(0, -1100, 0);
        gameObject.SetActive(false);
        OnResume?.Invoke();
    }

    public void Restart()
    {
        pauseMenu.transform.localPosition = new Vector3(0, -1100, 0);
        gameObject.SetActive(false);
        OnRestart?.Invoke();
    }

    public void BackHome()
    {
        OnBackHome?.Invoke();
    }

    private void OnEnable()
    {
        pauseMenu.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutCirc).SetLink(gameObject).SetUpdate(true);
        blocker.SetActive(true);
    }
}
