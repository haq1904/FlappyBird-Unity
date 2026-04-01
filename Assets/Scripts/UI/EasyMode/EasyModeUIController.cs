using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class EasyModeUIController : MonoBehaviour
{
    [SerializeField] private GameObject _timerPanel;
    public static event Action OnStartGame;
    
    public void OnEnable()
    {
        
        LogicManager.OnEasyMode += StartGame;
    }

    public void OnDisable()
    {
        OnStartGame = null;
        LogicManager.OnEasyMode -= StartGame;
    }

    public void StartGame(LogicManager.GameState gameState)
    {
        _timerPanel.SetActive(true);
        OnStartGame?.Invoke();
        
    }
}
