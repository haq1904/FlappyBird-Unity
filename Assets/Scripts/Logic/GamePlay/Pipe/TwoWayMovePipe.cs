using DG.Tweening;
using PlasticGui.WorkspaceWindow.Locks;
using UnityEngine;

public class TwoWayMovePipe : BasicPipe
{
    [SerializeField] private float timeToMove = 1;
    private bool isUp;

    protected override void OnEnable()
    {
        base.OnEnable();
        isUp = UnityEngine.Random.value > 0.5f;
        RevertDirection();
    }

    public void RevertDirection()
    {
        if (isUp)
        {
            transform.DOMoveY(heightRangeTop, timeToMove).SetEase(Ease.OutCubic).SetLink(gameObject);
            isUp = false;
            return;
        }
        transform.DOMoveY(heightRangeBot, timeToMove).SetEase(Ease.OutCubic).SetLink(gameObject);
        isUp = true;
    }

}
