
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnGamePause;

    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject timer;
    private void Start()
    {
        pauseBtn.onClick.AddListener(TogglePauseMenu);

    }

    private void TogglePauseMenu()
    {
        Sequence s = DOTween.Sequence();
        s.AppendCallback(() => pausePanel.SetActive(true));
        s.AppendInterval(0.15f);
        s.AppendCallback(()=>OnGamePause?.Invoke());
        Debug.Log("hehee");
    }

    public void StartCountDown()
    {
        if (timer.activeSelf)
        {
            timer.SetActive(false);
        }
        timer.SetActive(true);
    }
}
