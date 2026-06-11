using Cinemachine;
using System.Collections;
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

    [Header("Fields")]
    [SerializeField] private float timeForHitStop=1;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    
    [Header("Events")]
    [SerializeField] private UnityEvent OnStartGame;
    [SerializeField] private UnityEvent OnStartFlying;
    [SerializeField] private UnityEvent OnGameOver;

    [Header("Game State")]
    [SerializeField] private GameState gameState;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera Vcam1;



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

    public void GameOver()
    {
        Vcam1.Follow = null;
        StartCoroutine(HitStop(timeForHitStop));
        OnGameOver?.Invoke();
    }

    
    private IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    

   






}
