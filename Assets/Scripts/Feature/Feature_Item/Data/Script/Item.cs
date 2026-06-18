using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemEffect effect;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IReceivable>(out var gameObject))
        {
            effect.ApplyEffect(gameObject);
        }
    }

}
