using DG.Tweening;
using UnityEngine;


public class NitroPipe : BasePipe
{
    [Header("Fields")]
    [SerializeField] private float durationForBacking = 2;
    [SerializeField] private float timeSlowDown = 0.3f;
    [SerializeField] private float backDistance = 2;
    [SerializeField] private float speedForBooting = 3;
    [SerializeField] private Ease easeForBacking = Ease.Linear;

    [Header("Rotate")]
    [SerializeField] private Vector3 _desToRotate;
    [SerializeField] private float _durationForRotating = 1;
    [SerializeField] private Ease _easeForRotating = Ease.Linear;
    [SerializeField] private float _amplitudeForRotating = 1.7f;
    [SerializeField] private float _periodForRotating = 0;

    [Header("Game objects")]
    [SerializeField] private ParticleSystem _dustTop;
    [SerializeField] private ParticleSystem _dustBot;
    [SerializeField] private PartOfPipe _topPipe;
    [SerializeField] private PartOfPipe _botPipe;

    private Sequence s;
    private bool isTrigger = false;
    private Quaternion _resetRotation;

    protected override void Awake()
    {
        base.Awake();
        _resetRotation = _topPipe.transform.rotation;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _topPipe.transform.rotation = _resetRotation;
        _botPipe.transform.rotation = _resetRotation;
        isTrigger = false;

    }

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
            s.AppendCallback(() => { moveSpeed = 2.5f; });
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
                _topPipe.transform.DORotate(_desToRotate, _durationForRotating).SetEase(_easeForRotating, _amplitudeForRotating, _periodForRotating).SetLink(gameObject);
                _botPipe.transform.DORotate(new Vector3(_desToRotate.x, _desToRotate.y, -_desToRotate.z), _durationForRotating).SetEase(_easeForRotating, _amplitudeForRotating, _periodForRotating).SetLink(gameObject);
                moveSpeed = takeCurrSpeed + speedForBooting;
                _dustTop.Play();
                _dustBot.Play();

            });
            s.SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}
