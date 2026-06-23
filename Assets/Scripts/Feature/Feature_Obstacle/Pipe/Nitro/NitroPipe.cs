using UnityEngine;
using DG.Tweening;


public class NitroPipe : BasePipe
{
    [Header("Fields")]
    [SerializeField] private ParticleSystem _dustTop;
    [SerializeField] private ParticleSystem _dustBot;
    [SerializeField] private float durationForBacking = 2;
    [SerializeField] private float timeSlowDown = 0.3f;
    [SerializeField] private float backDistance = 2;
    [SerializeField] private float speedForBooting = 3;
    [SerializeField] private Ease easeForBacking = Ease.Linear;
    private Sequence s;
    private bool isTrigger = false;

    protected override void OnDisable()
    {
        base.OnDisable();
        s.Kill();
    }

    public override void GameOver()
    {
        base.GameOver();
        _dustBot.Stop();
        _dustTop.Stop();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        float takeCurrSpeed = moveSpeed;
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("ActionTriggerEnd") && !isTrigger)
        {
            isTrigger = true;
            s = DOTween.Sequence();
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
            s.AppendCallback(() => 
            { 
                moveSpeed = takeCurrSpeed + speedForBooting;
                _dustTop.Play();
                _dustBot.Play();
            });
            s.SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}
