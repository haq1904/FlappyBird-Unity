using UnityEngine;
using DG.Tweening;

public abstract class BasePipe : MonoBehaviour
{
    [SerializeField] protected float duration = 3;
    [SerializeField] protected float heightRangeTop = 0.5f;
    [SerializeField] protected float heightRangeBot = -5f;
    [SerializeField] protected Ease ease;
    [SerializeField] protected float targetX = -30;

    protected float randSpawnHeight;
    
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        randSpawnHeight = UnityEngine.Random.Range(heightRangeBot, heightRangeTop);
        transform.position = new Vector3(transform.position.x, randSpawnHeight, transform.position.z);
        rb.DOMoveX(targetX, duration).SetEase(ease).SetUpdate(UpdateType.Fixed).SetLink(gameObject);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PipeTrigger"))
        {
            Destroy(gameObject);
        } 
    }

}
