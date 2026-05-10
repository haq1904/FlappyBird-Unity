using UnityEngine;

public class HeadTriggerPipe : MonoBehaviour
{
    [SerializeField] private TwoWayMovePipe pipe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ActionTriggerRevert"))
        {
            pipe.RevertDirection();
        }
    }
}
