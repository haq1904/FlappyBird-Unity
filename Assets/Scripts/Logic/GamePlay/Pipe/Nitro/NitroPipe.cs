using UnityEngine;
using DG.Tweening;


public class NitroPipe : BasePipe
{
    [SerializeField] private float moveSpeedForBoot = 4;
    [SerializeField] private float durationForBacking = 2;
    [SerializeField] private float timeSlowDown = 0.3f;
    [SerializeField] private float backDistance = 2;
    [SerializeField] private Ease easeForBacking = Ease.Linear;
    //[SerializeField] private float overShot = 2;
    //[SerializeField] private Ease easeForForward = Ease.Linear;

    private bool isTrigger = false;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("ActionTriggerPipe") && !isTrigger)
        {
            isTrigger = true;
            Sequence s = DOTween.Sequence();
            s.AppendCallback(()=>{moveSpeed = 2.5f;});
            s.AppendInterval(timeSlowDown);
            s.AppendCallback(() => { moveSpeed = 2; });
            s.AppendInterval(timeSlowDown);
            s.AppendCallback(() => { moveSpeed = 1.5f; });
            s.AppendInterval(timeSlowDown);
            s.AppendCallback(() => { moveSpeed = 1; });
            s.AppendInterval(timeSlowDown);
            s.AppendCallback(() => { moveSpeed = 0.5f; });
            s.AppendInterval(timeSlowDown);
            s.AppendCallback(() => { moveSpeed = 0; });
            s.Append(rb.DOMoveX((transform.position.x + backDistance), durationForBacking).SetEase(easeForBacking).SetUpdate(UpdateType.Fixed).SetLink(gameObject));
            s.AppendCallback(() => {moveSpeed = moveSpeedForBoot;});
            s.SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}
