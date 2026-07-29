using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Statistic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _coin;

    private DataManagerService _dataManager;

    private void Awake()
    {
        _dataManager = FindAnyObjectByType<DataManagerService>();
    }

    private void OnEnable()
    {
        //_dataManager.SaveScore(20);
        Update_UI();
    }

    public void Update_UI()
    {
        if (_dataManager == null)
        {
            Debug.Log("Can not get Data manager.");
            return;
        }
        _score.text = _dataManager.GetBestScore().ToString();
        _coin.text = _dataManager.GetCoins().ToString();
    }
}
