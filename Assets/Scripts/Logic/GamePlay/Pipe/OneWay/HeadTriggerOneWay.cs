using DG.Tweening;
using PlasticGui.WorkspaceWindow.QueryViews;
using UnityEngine;

public class HeadTriggerOneWay : BaseHeadTrigger
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("ActionTriggerRevert"))
        {
            if(child!= null)
            { 
                child.Stop();
                return;
            }
            Debug.Log("Child is missing.");        
        }
    }
}
