using DG.Tweening;
using PlasticGui.WorkspaceWindow.Locks;
using UnityEngine;

public class TwoWayMovePipe : BasicPipe
{
    [SerializeField] private float timeToMove = 1;
    [SerializeField] private float unitYToMove = 10;
    [SerializeField] private ChildTwoWay child;
    public float GetTimeToMove { get => timeToMove; }
    public float GetUnitYToMove { get => unitYToMove; }

    protected override void StopMoving()
    {
        base.StopMoving();
        child.enabled = false;
    }

}
