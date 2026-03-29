using UnityEngine;
using System;

using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public int playerHighestScore = 0;
    public static LogicManager Instance;
    public GameState State;

    public static event Action<GameState> OnLobby;
    public static event Action<GameState> OnSelectMode;
    public static event Action<GameState> OnGameStart;
    public static event Action<GameState> OnGamePause;
    public static event Action<GameState> OnGameOver;
    public static event Action<GameState> OnGameRestart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Logic's instance has been created .  ");
            DontDestroyOnLoad(gameObject);
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnDisable()
    {
       
    }


    private void ClearAllEvents()
    {
        OnLobby = null;
        OnGameStart = null;
        OnGameRestart = null;
        OnGamePause = null;
        OnGameOver = null;
        OnSelectMode = null;
        Debug.Log("All LogicManager's event have been cleared");
    }

    private void Start()
    {      
    }

    public void Lobby()
    {
        UpdateGameState(GameState.Lobby);
    }
    public void SelectMode()
    {
        UpdateGameState(GameState.SelectMode);
    }

    public void StartGame()
    {
        UpdateGameState(GameState.StartGame);
    }

    public void PauseGame()
    {
        UpdateGameState(GameState.Pause);
    }

    [ContextMenu("Increase score")]
    public void AddScore(int score)
    {
        playerHighestScore += score;
    }

    public void RestartGame()
    {
      
        
    }

    public void ResetGame()
    {
        ClearAllEvents();
    
        
    }

    public void GameOver()
    {
        UpdateGameState(GameState.GameOver);
    }

 

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Lobby:
                OnLobby?.Invoke(newState);
                Debug.Log("Game state changed : Lobby");
                break;
            case GameState.SelectMode:
                OnSelectMode?.Invoke(newState);
                Debug.Log("Game State chaged : Select Mode");
                break;
            case GameState.StartGame:
                OnGameStart?.Invoke(newState);
                Debug.Log("Game state changed : Start Game");
                break;
            case GameState.GameOver:
                OnGameOver?.Invoke(newState);
                Debug.Log("Game state changed : Game Over");
                break;
            case GameState.Pause:
                OnGamePause?.Invoke(newState);
                Debug.Log("Game state changed : Pause");
                break;
            case GameState.Restart:
                OnGameRestart?.Invoke(newState);
                Debug.Log("Game state changed : Restart");
                break;
        }

    }

    public enum GameState
    {
        Lobby,
        SelectMode,
        StartGame,
        GameOver,
        Pause,
        Restart
    }

}
