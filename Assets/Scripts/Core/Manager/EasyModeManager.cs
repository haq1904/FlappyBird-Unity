using JetBrains.Annotations;
using log4net;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class EasyModeManager : MonoBehaviour
{
    public static event Action OnStartGame;
    public void OnEnable()
    {
        LogicManager.OnEasyMode += StartGame;
    }

    public void OnDisable()
    {
        
        LogicManager.OnEasyMode -= StartGame;
        OnStartGame = null;
    }


    private void StartGame(LogicManager.GameState state)
    {
        OnStartGame?.Invoke();
    }

   






}
