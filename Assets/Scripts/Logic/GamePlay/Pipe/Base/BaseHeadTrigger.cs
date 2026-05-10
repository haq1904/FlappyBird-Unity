using DG.Tweening;
using UnityEngine;

public abstract class BaseHeadTrigger : MonoBehaviour
{
    [SerializeField] protected ChildOneWay child;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
         
    }
}

