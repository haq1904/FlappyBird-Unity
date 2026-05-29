
using DG.Tweening;
using UnityEngine;

public class GhostPipe : BasePipe
{
    [SerializeField] private float timeToFade = 1;
    [SerializeField] private float timeToAppear = 1;
    [SerializeField] private Ease easeToFade = Ease.Linear;
    [SerializeField] private Ease easeToAppear = Ease.Linear;
    [SerializeField] private SpriteRenderer spriteTopPipe;
    [SerializeField] private SpriteRenderer spriteBotPipe;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("ActionTriggerStart"))
        {
            spriteBotPipe.DOFade(0, timeToFade).SetEase(easeToFade).SetLink(gameObject);
            spriteTopPipe.DOFade(0, timeToFade).SetEase(easeToFade).SetLink(gameObject);

        }
        else if (collision.CompareTag("ActionTriggerEnd"))
        {
            spriteBotPipe.DOFade(1, timeToAppear).SetEase(easeToAppear).SetLink(gameObject);
            spriteTopPipe.DOFade(1, timeToAppear).SetEase(easeToAppear).SetLink(gameObject);
        }
    }
}
