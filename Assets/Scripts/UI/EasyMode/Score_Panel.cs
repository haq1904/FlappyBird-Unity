using TMPro;
using UnityEngine;

public class Score_Panel : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI scoreText;


    public void HandleChangePoint(float point)
    {
        animator.Play("AddPoint",-1,0f);
        scoreText.text = point.ToString();
    }
}
