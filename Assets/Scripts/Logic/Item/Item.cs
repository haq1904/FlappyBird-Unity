using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemEffectStrategy effect;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IReceivable>(out var gameObject))
        {
            effect.ApplyEffect(gameObject);
        }
    }

}
