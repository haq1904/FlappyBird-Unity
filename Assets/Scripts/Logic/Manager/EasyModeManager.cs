using UnityEngine;
using UnityEngine.Events;
public class EasyModeManager : MonoBehaviour
{

    public enum GameState
    {
        GameStart,
        GamePause,
        GameOver,
    }


    [Header("Events")]
    [SerializeField] private UnityEvent OnStartGame;
    [SerializeField] private UnityEvent OnStartFlying;


    [Header("Game State")]
    [SerializeField] private GameState gameState;


    public void Awake()
    {
        
    }

    public void Start()
    { 
        UpdateGameState(GameState.GameStart);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.GameStart:
                OnStartGame?.Invoke();
                break;
            case GameState.GamePause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void StartFlying()
    {
        OnStartFlying?.Invoke();
    }

    

    

   






}
