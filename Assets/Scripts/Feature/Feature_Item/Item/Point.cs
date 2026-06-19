using System.Xml.Serialization;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Video;

public class Point : ItemService
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IReceivable>(out var gameObj))
        {
            ApplyEffect(gameObj);
        }
    }

    public override void ApplyEffect(IReceivable gameObj)
    {
        effect.ApplyEffect(gameObj);
    }
}
