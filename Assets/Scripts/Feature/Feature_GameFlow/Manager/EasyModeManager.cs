using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class EasyModeManager : MonoBehaviour
{

    public enum GameState
    {
        GameStart,
        GameResume,
        GamePause,
        GameOver,
    }

    [Header("Fields")]
    [SerializeField] private float timeForHitStop=1;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [Header("Game State")]
    [SerializeField] private GameState gameState;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera Vcam1;

    [Header("Events")]
    [SerializeField] private UnityEvent OnStartGame;
    [SerializeField] private UnityEvent OnGameResume;
    [SerializeField] private UnityEvent OnStartFlying;
    [SerializeField] private UnityEvent OnGameOver;
    [SerializeField] private UnityEvent OnGamePause;

    




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
            case GameState.GameResume:
                OnGameResume?.Invoke();
                break;
            case GameState.GamePause:
                OnGamePause?.Invoke();
                break;
            case GameState.GameOver:
                OnGameOver?.Invoke();
                break;
        }
    }

    public void StartGame()
    {
        UpdateGameState(GameState.GameStart);
    }

    public void GameOver()
    {
        Vcam1.Follow = null;
        StartCoroutine(HitStop(timeForHitStop));
        UpdateGameState(GameState.GameOver);
    }

    public void GamePause()
    {
        UpdateGameState(GameState.GamePause);
    }

    public void GameResume()
    {
        UpdateGameState(GameState.GameResume);
    }
    public void StartFlying()
    {
        OnStartFlying?.Invoke();
    }

    public void BackHome()
    {
        Debug.Log("Back to loppy");
    }
    
    private IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    

   






}
