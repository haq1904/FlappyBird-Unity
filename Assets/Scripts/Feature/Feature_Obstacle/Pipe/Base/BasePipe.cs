using UnityEngine;
using DG.Tweening;
using UnityEditor;

public abstract class BasePipe : ObstacleService
{
    [Header("Fields")]
    [SerializeField] protected float moveSpeed = 3;
    protected float heightRangeTop = 2.6f;
    protected float heightRangeBot = -2.6f;
    protected float randSpawnHeight;


    public float GetHeightRange { get => heightRangeTop; }
    
    protected Rigidbody2D rb;

    protected float currSpeed;

    #region MonoBehavior function
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();       
    }

    protected virtual void OnEnable()
    {
        randSpawnHeight = UnityEngine.Random.Range(heightRangeBot, heightRangeTop);
        transform.position = new Vector3(transform.position.x, randSpawnHeight, transform.position.z);   
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void FixedUpdate()
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
    #endregion

    #region Implement interface IObstacle
    public override void SetSpeed(float moveSpeed = 0)//Implement interface IObstacle
    {
        this.moveSpeed = moveSpeed;
    }
    public override float GetSpawnHeight()
    {
        return randSpawnHeight;
    }
    #endregion

    #region State
    public virtual void GameOver()
    {
        rb.linearVelocity = Vector2.zero;
        this.enabled = false;
    }

    public virtual void GameRestart()
    {
        Destroy(gameObject);
    }

    
    #endregion
}
