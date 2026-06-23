using DG.Tweening;
using JetBrains.Annotations;
using System.Xml.Serialization;
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

    private Vector2 _coinResetAnchoredPos;


    private void Start()
    {
        _coinResetAnchoredPos = _coin.rectTransform.anchoredPosition;
    }

    public void HandleChangePoint(float point)
    {
        _score.text = point.ToString();
        transform.DOShakePosition(duration:duration, strength:strength, vibrato:vibrato, randomness:randomness, snapping:snapping,fadeOut:fadeOut).SetLink(gameObject);
    }

    public void HandleChangeCoin(float coin)
    {
        _coin.text = coin.ToString();
        ShakeCoinText();
    }

    private void ShakeCoinText()
    {
        //kill pre dotween and reset coin text anchored position to prevent drift from overflapping shakes
        _coin.rectTransform.DOKill();
        _coin.rectTransform.anchoredPosition = _coinResetAnchoredPos; 
        _coin.rectTransform.DOShakeAnchorPos(duration: duration, strength: strength, vibrato: vibrato, randomness: randomness, snapping: snapping, fadeOut: fadeOut).SetLink(gameObject);
    }
}
