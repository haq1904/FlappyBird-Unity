using UnityEngine;

public class PacingDesign : MonoBehaviour
{

    [SerializeField] private PacingScenario[] easeScenario;

    [SerializeField] private PacingScenario[] normalScenario;

    [SerializeField] private PacingScenario[] hardScenario;

    public void OnStartGame()//Receives event from EasyModeManager
    {
        Debug.Log("Received event");
    }
}
