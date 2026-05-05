using UnityEngine;

public abstract class BasePipe : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1;
    [SerializeField] protected float heightRangeTop = 0.5f;
    [SerializeField] protected float heightRangeBot = -5f;
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    protected virtual void FixedUpdate()
    {
        MoveLeft();
    }

    protected virtual void MoveLeft()
    {
        rb.linearVelocity = Vector2.left * moveSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PipeTrigger"))
        {
            Destroy(gameObject);
        } 
    }

}
