using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUp_Controller : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnGamePause;
    [SerializeField] private SoundTypeGameEvent OnRaiseSound;

    [Header("Game objects")]
    [SerializeField] private Game_Over_Panel gameOverPanel;

    private Button[] _allButton;

    private void Start()
    {
        _allButton = GetComponentsInChildren<Button>(true);
        foreach (Button btn in _allButton)
        {
            btn.onClick.AddListener(() => PlaySound(SoundType.Click));
        }
    }

    public void GameOver()
    {
        gameOverPanel.TurnOn();
    }

    private void PlaySound(SoundType soundType)
    {
        OnRaiseSound?.Raise(soundType);
    }

}
