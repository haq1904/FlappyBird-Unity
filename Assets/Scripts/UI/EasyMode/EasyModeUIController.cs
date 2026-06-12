
using UnityEngine;
using UnityEngine.Events;

public class EasyModeUIController : MonoBehaviour
{
    public enum EasyModeGameState
    {
        OnStartGame,
        OnPlayAgain,
    }



    [Header("Event")]
    [SerializeField] private UnityEvent OnCountDown;
    
    public void OnStartGame() //Receive OnStartEasyMode event from EasyModeManager
    {
        OnCountDown?.Invoke();
    }


    
}
