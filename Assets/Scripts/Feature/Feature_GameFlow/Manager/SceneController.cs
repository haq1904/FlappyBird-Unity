
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public async void LoadScene(int indexScene)
    {

        var scene = SceneManager.LoadSceneAsync(indexScene);

        LoadingScreenController.Instance.LoadingScrollDown(false);

        scene.allowSceneActivation = false;

        await Task.Delay((int)LoadingScreenController.Instance.GetMinimumMoveDuration() * 1000);

        scene.allowSceneActivation = true;
        Time.timeScale = 1;


    }


}
