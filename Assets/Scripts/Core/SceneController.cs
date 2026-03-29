
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Codice.Client.Common.GameUI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;

    public static SceneController Instance;

    private void Awake()
    {
        if(Instance == null)
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
        _target = 0f;
        _progressBar.fillAmount = 0f;

        _loaderCanvas.SetActive(true);

        var scene = SceneManager.LoadSceneAsync(indexScene);
        
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < 0.9f);
         
        

        _progressBar.fillAmount = 1f;

        await Task.Delay(1000);

        scene.allowSceneActivation = true;

        _loaderCanvas.SetActive(false);
    }


    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }

    
}
