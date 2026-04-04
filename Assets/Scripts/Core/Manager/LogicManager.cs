using UnityEngine;
using System;

using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    [SerializeField] private bool MoveToEasyMode = false;

    public int playerHighestScore = 0;
    public static LogicManager Instance;
    public GameState State;

    public static event Action<GameState> OnLobby;
    public static event Action<GameState> OnEasyMode;
    public static event Action<GameState> OnHardMode;
    

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (!MoveToEasyMode)
                {
                    SceneManager.LoadScene("LobbyScene");
                    
                }
                else if (MoveToEasyMode)
                {
                    SceneManager.LoadScene("EasyModeScene");
                    
                }

            }

        }
        else
        {
            Destroy(gameObject);
            
        }
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EasyModeScene")
            EasyMode();

        else if (scene.name == "LobbyScene")
            Lobby();
        
    }

    private void OnEnable(){
        
    }

    private void OnDisable()
    {
       ClearAllEvents();
    }


    private void ClearAllEvents()
    {
        OnLobby = null;
        OnEasyMode = null;
        OnHardMode = null;
        Debug.Log("All LogicManager's event have been cleared");
    }

    private void Start()
    {      
    }

    public void Lobby()
    {
        UpdateGameState(GameState.Lobby);
    }
    

    public void EasyMode()
    {
        UpdateGameState(GameState.EasyMode);
    }

    public void HardMode()
    {
        UpdateGameState(GameState.HardMode);
    }
    public void PauseGame()
    {
        UpdateGameState(GameState.Pause);
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
                break;
            
            case GameState.EasyMode:
                OnEasyMode?.Invoke(newState);
                break;
            case GameState.HardMode:
                OnHardMode?.Invoke(newState);
                break;
            
        }

    }

    public enum GameState
    {
        Lobby,
        EasyMode,
        HardMode,
        GameOver,
        Pause,
        Restart
    }

}
