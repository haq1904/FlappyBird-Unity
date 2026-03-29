using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeScene(int indexScene)
    {
        switch (indexScene)
        {
            case 1:
                Debug.Log("Back to Lobby");
                SceneController.Instance.LoadScene(1);
                break;
            case 2:
                Debug.Log("Move to Easy Mode");
                SceneController.Instance.LoadScene(2);
                break;
            case 3:
                Debug.Log("Move to Hard Mode");
                SceneController.Instance.LoadScene(3);
                break;
            default:
                Debug.Log("Invalid index of scenes");
                break;
        }
    }
}
