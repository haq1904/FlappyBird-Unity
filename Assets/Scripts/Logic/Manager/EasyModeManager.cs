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
                Debug.Log("EasyMode's logic notified -> OnStartEasyMode event is raised");
                break;
            case GameState.GamePause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void StartFlying()
    {
        Debug.Log("EasyMode's logic received OnTimeDone event and start flying");
        Debug.Log("Bird is allowed to fly");
    }

    

    

   






}
