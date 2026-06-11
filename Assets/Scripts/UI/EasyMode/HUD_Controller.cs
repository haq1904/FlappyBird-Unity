using DG.Tweening;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pausePanel;
    private void Start()
    {
        pauseBtn.onClick.AddListener(TogglePauseMenu);

    }

    private void TogglePauseMenu()
    {
        pausePanel.SetActive(true);
    }
}
