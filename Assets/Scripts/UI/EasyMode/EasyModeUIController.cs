
using Codice.Client.Common.GameUI;
using System;
using System.Timers;
using UnityEngine;


public class EasyModeUIController : MonoBehaviour
{
    [SerializeField] private GameObject _timerPanel;
    public static Action OnStartGame;
    
    public void OnEnable()
    {
        StartCountDown();
        Timer.OnDone += FinishedCountDown;
    }

    public void OnDisable()
    {
        OnStartGame = null;
        Timer.OnDone -= FinishedCountDown;
    }
    public void StartCountDown()
    {
        _timerPanel.SetActive(true);
        Debug.Log("Set active Timer panel.");

    }

    public void FinishedCountDown()
    {
        OnStartGame?.Invoke();
        Debug.Log("Finished count down.");
    }
}
