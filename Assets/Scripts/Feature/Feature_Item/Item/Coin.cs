using UnityEngine;

public class Coin : Item
{

    [SerializeField] private Animator _animator; 
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IReceivable>(out var gameObject))
        {
            _animator.Play("Taken", -1, 0f);
        }
    }
}
