
using System;
using UnityEngine;


public class EasyModeUIController : MonoBehaviour
{
    [SerializeField] private GameObject _timerPanel;
    public static event Action OnStartGame;
    
    public void OnEnable()
    {
        StartGame();
    }

    public void OnDisable()
    {
        OnStartGame = null;      
    }

    public void StartGame()
    {
        _timerPanel.SetActive(true);
        OnStartGame?.Invoke();
        
    }
}
