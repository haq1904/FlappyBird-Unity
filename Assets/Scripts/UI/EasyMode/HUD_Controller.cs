
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnGamePause;

    [Header("Game objects")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Pause_Panel pausePanel;
    [SerializeField] private PlayerStats_Controller stats;
    [SerializeField] private GameObject timer;
    private void Start()
    {
        pauseBtn.onClick.AddListener(TogglePauseMenu);

    }

    private void TogglePauseMenu()
    {
        OnGamePause?.Invoke();
        pausePanel.TurnOn();
    }

    public void HandleCountDown()
    {
        timer.SetActive(true);
    }

    public void HandleChangePoint(float point)
    {
        stats.HandleChangePoint(point);
    }

    public void HandleChangeCoin(float coin)
    {
        stats.HandleChangeCoin(coin);
    }

    public void HandleRestart()
    {
        timer.SetActive(false);
        HandleCountDown();
    }
}
