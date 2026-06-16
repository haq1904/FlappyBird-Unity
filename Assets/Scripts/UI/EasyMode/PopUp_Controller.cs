using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUp_Controller : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnGamePause;

    [Header("Game objects")]
    [SerializeField] private Game_Over_Panel gameOverPanel;

    public void GameOver()
    {
        Sequence s = DOTween.Sequence();
        s.SetUpdate(true);
        s.AppendInterval(1f);
        s.AppendCallback(() => { gameOverPanel.TurnOn(); });
    }
}
