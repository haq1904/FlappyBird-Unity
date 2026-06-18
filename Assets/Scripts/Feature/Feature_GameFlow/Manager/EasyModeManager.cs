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
        GameRestart,
        GamePause,
        GameOver,
    }

    [Header("Fields")]
    [SerializeField] private float currPoint = 0;
    [SerializeField] private float currCoin = 0;
    [SerializeField] private float timeForHitStop=1;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [Header("Game State")]
    [SerializeField] private GameState gameState;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera Vcam1;

    [Header("Events")]
    [SerializeField] private UnityEvent OnStartGame;
    [SerializeField] private UnityEvent OnStartFlying;
    [SerializeField] private UnityEvent OnGameRestart;
    [SerializeField] private UnityEvent OnGameOver;
    [SerializeField] private UnityEvent OnGamePause;
    [SerializeField] private UnityEvent OnResume;
    [SerializeField] private FloatGameEvent OnChangePoint;
    [SerializeField] private FloatGameEvent OnChangeCoin;

    private Transform followTransform;
    public void Start()
    { 
        UpdateGameState(GameState.GameStart);
        followTransform = Vcam1.Follow;
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
                OnResume?.Invoke();
                break;
            case GameState.GameRestart:
                OnGameRestart?.Invoke();
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
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        UpdateGameState(GameState.GameResume);
        Time.timeScale = 1f;
    }

    public void GameRestart()
    {
        UpdateGameState(GameState.GameRestart);
        Vcam1.Follow = followTransform;
        currPoint = 0;
        HandleChangePoint(0f);
        Time.timeScale = 1f;
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


    public void HandleChangePoint(float point)
    {
        currPoint += point;
        OnChangePoint?.Raise(currPoint);
    }
    
    public void HandleChangeCoin(float coin)
    {
        currCoin += coin;
        OnChangeCoin?.Raise(coin);
    }

   






}
