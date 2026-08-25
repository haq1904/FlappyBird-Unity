using DG.Tweening;
using UnityEditor;
using UnityEngine;

public abstract class BasePipe : ObstacleService
{
    [Header("Fields")]
    [SerializeField] protected float moveSpeed = 3;
    protected float heightRangeTop = 3f;
    protected float heightRangeBot = -3f;
    protected float randSpawnHeight;


    public float GetHeightRange { get => heightRangeTop; }

    protected Rigidbody2D rb;

    protected float currSpeed;
    protected ObjectPoolingService _poolService;
    protected bool _isGameOver = false;

    #region MonoBehavior function
    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _poolService = FindAnyObjectByType<ObjectPoolingService>();
        if (_poolService == null)
            Debug.Log("Can not get pool service");
    }

    protected virtual void OnEnable()
    {
        _isGameOver = false;
        randSpawnHeight = UnityEngine.Random.Range(heightRangeBot, heightRangeTop);
        transform.position = new Vector3(transform.position.x, randSpawnHeight, transform.position.z);
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void FixedUpdate()
    {
        if (!_isGameOver)
            rb.linearVelocity = Vector2.left * moveSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PoolCollector") && gameObject.activeSelf)
        {
            if (_poolService != null)
                _poolService.ReturnObjectToPool(gameObject);
            else
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
        _isGameOver = true;
        rb.linearVelocity = Vector2.zero;
    }

    public virtual void GameRestart()
    {
        if (!gameObject.activeSelf) return;

        if (_poolService != null)
            _poolService.ReturnObjectToPool(gameObject);
        else
            Destroy(gameObject);
    }



    #endregion
}
