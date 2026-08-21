using UnityEngine;

public class ZigzagStorm : BaseStorm
{
    [Header("Zigzag Settings")]
    [Tooltip("Tốc độ bay lên/xuống")]
    [SerializeField] private float _verticalSpeed = 3f;

    private int _directionY = 1;

    private void Start()
    {
        _directionY = Random.value > 0.5f ? 1 : -1;
    }

    protected override void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(-1f, _verticalSpeed * _directionY)*_moveSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
        if (collision.CompareTag("ActionTriggerRevert"))
        {
            _directionY *= -1;
        }
    }
}