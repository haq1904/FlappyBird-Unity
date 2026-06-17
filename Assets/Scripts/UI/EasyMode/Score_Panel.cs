using TMPro;
using UnityEngine;

public class Score_Panel : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI scoreText;


    public void HandleChangePoint(float point)
    {
        scoreText.text = point.ToString();
    }
}
