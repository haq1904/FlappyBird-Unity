using UnityEngine;
using System;

using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    [SerializeField] private bool MoveToEasyMode = false;
    public static LogicManager Instance;
    public GameState State;

    

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (!MoveToEasyMode)
                {
                    SceneManager.LoadScene("LobbyScene");
                    State = GameState.Lobby;
                }
                else if (MoveToEasyMode)
                {
                    SceneManager.LoadScene("EasyModeScene");
                    State = GameState.EasyMode;
                }

            }

        }
        else
        {
            Destroy(gameObject);
            
        }
        
    }

    

    private void OnEnable(){        
    }

    private void OnDisable()
    {
       ClearAllEvents();
    }


    private void ClearAllEvents()
    {
        Debug.Log("All LogicManager's event have been cleared");
    }

    

    
    public void Lobby(){
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
    

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Lobby:
                SceneController.Instance.LoadScene(1);
                break; 
            case GameState.EasyMode:
                SceneController.Instance.LoadScene(2);
                break;
            case GameState.HardMode:
                SceneController.Instance.LoadScene(3);
                break;
            
        }

    }

    public enum GameState
    {
        Lobby,
        EasyMode,
        HardMode,
    }

}


