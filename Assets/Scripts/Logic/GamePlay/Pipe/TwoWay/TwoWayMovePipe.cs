using DG.Tweening;
using PlasticGui.WorkspaceWindow.Locks;
using UnityEngine;

public class TwoWayMovePipe : BasicPipe
{
    [SerializeField] private float timeToMove = 1;
    [SerializeField] private float unitYToMove = 10;
    public float GetTimeToMove { get => timeToMove; }
    public float GetUnitYToMove { get => unitYToMove; }
    

}
