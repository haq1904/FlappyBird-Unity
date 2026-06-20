using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats_Controller : MonoBehaviour
{
    [Header("Fields for shake")]
    [SerializeField] float duration = 1;
    [SerializeField] Vector3 strength;
    [SerializeField] int vibrato = 1;
    [SerializeField] float randomness = 90;
    [SerializeField] bool snapping = false;
    [SerializeField] bool fadeOut = true;
    
    [Header("Game objects")]
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _coin;
    [SerializeField] private Image _coinImg;


    public void HandleChangePoint(float point)
    {
        _score.text = point.ToString();
        transform.DOShakePosition(duration:duration, strength:strength, vibrato:vibrato, randomness:randomness, snapping:snapping,fadeOut:fadeOut);
    }

    public void HandleChangeCoin(float coin)
    {
        _coin.text = coin.ToString();
    }
}
