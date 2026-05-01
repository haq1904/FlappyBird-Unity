
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
        
        _timer.StartCountDown();
    }


    
}
