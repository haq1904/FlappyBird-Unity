using DG.Tweening;
using UnityEngine;

public class Coin : ItemService
{
    [Header("Fields")]
    [SerializeField] private float _force;
    [SerializeField] private float _timeToFade=1;
    [SerializeField] private Ease _easeToFade = Ease.OutElastic;
    [SerializeField] private SpriteRenderer spr;

    private Rigidbody2D _rb;
    private bool _IsTaken=false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IReceivable>(out var gameObj) && !_IsTaken )
        {
            ApplyEffect(gameObj);
            float finalX = UnityEngine.Random.Range(-0.5f, 0.25f);
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(new Vector2(finalX, 1)*_force, ForceMode2D.Impulse);
            spr.DOFade(0, _timeToFade).SetEase(_easeToFade).SetLink(gameObject);
            _IsTaken = true;
        }
    }

    public override void ApplyEffect(IReceivable gameObj)
    {
        effect.ApplyEffect(gameObj);
    }
}
