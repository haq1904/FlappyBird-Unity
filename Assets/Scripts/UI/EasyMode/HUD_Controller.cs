
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
    private void Start()
    {
        pauseBtn.onClick.AddListener(TogglePauseMenu);

    }

    private void TogglePauseMenu()
    {
        pausePanel.SetActive(true);
        OnGamePause?.Invoke();
    }
}
