using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeScene(int indexScene)
    {
        SceneController.Instance.EnableScript();       
        switch (indexScene)
        {
            case 1:
                LogicManager.Instance.UpdateGameState(LogicManager.GameState.Lobby);
                SceneController.Instance.LoadScene(1);                
                break;
            case 2:
                LogicManager.Instance.UpdateGameState(LogicManager.GameState.EasyMode);
                SceneController.Instance.LoadScene(2);              
                break;
            case 3:
                LogicManager.Instance.UpdateGameState(LogicManager.GameState.HardMode);
                SceneController.Instance.LoadScene(3);     
                break;
            default:
                Debug.Log("Invalid index of scenes");
                break;
        }
    }
}
