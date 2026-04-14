
using System;
using UnityEngine;
using UnityEngine.Events;

public class EasyModeUIController : MonoBehaviour
{
    public enum EasyModeGameState
    {
        OnStartGame,
        OnPlayAgain,
    }

    

    [Header("Panels")]
    [SerializeField] private Timer _timer;
    
    public void OnStartGame()
    {
        Debug.Log("EasyModeUIController received OnStartEasyMode event and start countdown.");
        _timer.StartCountDown();
    }


    
}
