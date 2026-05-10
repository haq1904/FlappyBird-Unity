using UnityEngine;

public class NitroPipe : BasePipe
{
    [SerializeField] private float moveSpeedForBoot = 4;
    [SerializeField] private float backDistance = 2;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("ActionPipeTrigger"))
        {
            
        }
    }
}
