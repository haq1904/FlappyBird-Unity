using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Pause_Panel : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent OnResume;

    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resume;

    private void Start()
    {
        resume.onClick.AddListener(Resume);
    }

    public void Resume()
    {
        pauseMenu.transform.localPosition = new Vector3(0, -1100, 0);
        gameObject.SetActive(false);
        OnResume?.Invoke();
    }

    private void OnEnable()
    {
        pauseMenu.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutCirc).SetLink(gameObject);
        blocker.SetActive(true);
    }
}
