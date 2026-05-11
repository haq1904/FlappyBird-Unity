using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class OneWayMovePipe : BasePipe
{
    [SerializeField] private ChildOneWay child;
    [SerializeField] protected float unitToMove = 1f;
    public float GetUnitToMove { get => unitToMove; }

    protected override void OnEnable()
    {
        base.OnEnable();
        child.enabled = true;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("ActionTriggerPipe"))
        {
            child.MoveUpDown();
        }
        base.OnTriggerEnter2D(collision);
    }
}
