using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    [SerializeField] private bool MoveToEasyMode = false;
    public static LogicManager Instance;
    public GameState State;



    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Chỉ chạy logic chuyển scene nếu đây là bản thể chính (không bị Destroy)
        if (Instance == this)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (!MoveToEasyMode)
                {
                    SceneController.Instance.LoadScene(1);
                    State = GameState.Lobby;
                }
                else
                {
                    SceneController.Instance.LoadScene(2);
                    State = GameState.EasyMode;
                }
            }
        }
    }



    private void OnEnable()
    {
    }

    private void OnDisable()
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


