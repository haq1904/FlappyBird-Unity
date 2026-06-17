using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Score_Panel : MonoBehaviour
{
    [Header("Fields for shake")]
    [SerializeField] float duration = 1;
    [SerializeField] Vector3 strength;
    [SerializeField] int vibrato = 1;
    [SerializeField] float randomness = 90;
    [SerializeField] bool snapping = false;
    [SerializeField] bool fadeOut = true;
    
    [Header("Game objects")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI scoreText;


    public void HandleChangePoint(float point)
    {
        animator.Play("AddPoint",-1,0f);
        scoreText.text = point.ToString();
        gameObject.transform.DOShakePosition(duration:duration, strength:strength, vibrato:vibrato, randomness:randomness, snapping:snapping,fadeOut:fadeOut);
    }
}
