using System.Diagnostics.Contracts;
using UnityEngine;

public class BaseAttackingBird : ObstacleService
{

    private float _moveSpeed = 1;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.left * _moveSpeed;
    }


    public void HandleRestart()
    {
        Destroy(gameObject);
    }

    #region Override ObstacleService
    public override void SetSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public override float GetSpawnHeight()
    {
        return transform.position.y;
    }
    #endregion
}
