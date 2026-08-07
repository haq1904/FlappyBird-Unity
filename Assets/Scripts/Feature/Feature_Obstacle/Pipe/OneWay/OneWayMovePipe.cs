using DG.Tweening;
using JetBrains.Annotations;
using Unity.VectorGraphics;
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

        if (collision.CompareTag("ActionTriggerEnd"))
        {
            child.MoveUpDown();
        }
        base.OnTriggerEnter2D(collision);
    }

    public override void GameOver()
    {
        base.GameOver();
        child.enabled = false;

    }

}
