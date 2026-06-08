using UnityEngine;

public class HeadTriggerPipe : MonoBehaviour
{
    [SerializeField] private ChildTwoWay child;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ActionTriggerRevert"))
        {
            child.RevertDirection();
        }
    }
}
