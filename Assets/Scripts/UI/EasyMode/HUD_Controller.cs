
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
    [SerializeField] private Score_Panel scorePanel;
    [SerializeField] private Timer timer;
    private void Start()
    {
        pauseBtn.onClick.AddListener(TogglePauseMenu);

    }

    private void TogglePauseMenu()
    {
        Sequence s = DOTween.Sequence();
        s.AppendCallback(() => pausePanel.TurnOn());
        s.AppendInterval(0.15f);
        s.AppendCallback(()=>OnGamePause?.Invoke());
    }

    public void HandleCountDown()
    {
        timer.TurnOn();
    }

    public void HandleChangePoint(float point)
    {
        scorePanel.HandleChangePoint(point);
    }

    public void HandleRestart()
    {
        timer.TurnOff();
        HandleCountDown();
    }
}
