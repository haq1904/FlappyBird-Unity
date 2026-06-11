using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        pauseBtn.onClick.AddListener(TogglePauseMenu);
    }

    private void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
