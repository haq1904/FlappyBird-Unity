using DG.Tweening;
using Unity.Plastic.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Panel : MonoBehaviour
{
    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resume;

    private void Start()
    {
        resume.onClick.AddListener(Resume);
    }

    public void Resume()
    {
        pauseMenu.transform.localPosition = new Vector3(0, -1100, 0);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        pauseMenu.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutCirc).SetLink(gameObject);
        blocker.SetActive(true);
    }
}
