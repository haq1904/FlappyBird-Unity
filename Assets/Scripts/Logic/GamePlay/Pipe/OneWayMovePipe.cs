using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class OneWayMovePipe : BasePipe
{
    [SerializeField] private float timeToMove = 1;
    [SerializeField] private float distanceToMove = 1;
    private float yToMove;
    protected override void OnEnable()
    {
        base.OnEnable();
        do{
            yToMove = UnityEngine.Random.Range(heightRangeBot, heightRangeTop);
        } while (Mathf.Abs(yToMove - randSpawnHeight) < distanceToMove);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("ActionTriggerPipe"))
        {
            MoveUpDown();
        }
        base.OnTriggerEnter2D(collision);
    }


    private void MoveUpDown()
    {
        transform.DOMoveY(yToMove, timeToMove).SetEase(Ease.OutCubic).SetLink(gameObject);
    }
}
