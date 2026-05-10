using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class OneWayMovePipe : BasePipe
{
    [SerializeField] private ChildOneWay child;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("ActionTriggerPipe"))
        {
            child.MoveUpDown();
        }
        base.OnTriggerEnter2D(collision);
    }
}
