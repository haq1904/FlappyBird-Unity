using System.Collections;
using Cinemachine;
using DG.Tweening;
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
    [SerializeField] private float timeForHitStop = 1;
    [SerializeField] private GameObject vcamTarget;

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
    private DataManagerService _dataManager;

    private void Awake()
    {
        _dataManager = FindAnyObjectByType<DataManagerService>();
        if (_dataManager == null)
            Debug.Log("Can not get data manager.");
    }

    public void Start()
    {
        UpdateGameState(GameState.GameStart);
        followTransform = Vcam1.Follow;
        vcamTarget.transform.position = Vcam1.Follow.position;
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
        _dataManager.SaveScore(currPoint);
        _dataManager.AddCoins(currCoin);

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
        LoadingScreenController.Instance.LoadingBoth();
        DOVirtual.DelayedCall(LoadingScreenController.Instance.GetMinimumMoveDuration(), () =>
        {
            UpdateGameState(GameState.GameRestart);
            Time.timeScale = 1;
            _dataManager.SaveScore(currPoint);
            currPoint = 0;
            currCoin = 0;
            HandleChangePoint(0f);
            HandleChangeCoin(0f);
            Vcam1.Follow = vcamTarget.transform;
        });

    }

    public void StartFlying()
    {
        OnStartFlying?.Invoke();
        Vcam1.Follow = followTransform;
    }

    public void BackHome()
    {
        SceneController.Instance.LoadScene(1);
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
        OnChangeCoin?.Raise(currCoin);
    }








}
