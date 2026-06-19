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
        _effect.ApplyEffect(gameObj);
    }

    public override void SetSpeed(float speed)
    {
        _speed = speed; 
    }
}
