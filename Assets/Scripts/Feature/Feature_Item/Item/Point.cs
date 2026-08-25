using System.Xml.Serialization;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Video;

public class Point : ItemService
{


    private bool _isTaken = false;

    private void OnEnable()
    {
        _isTaken = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTaken) return;

        if (collision.TryGetComponent<IReceivable>(out var gameObj))
        {
            _isTaken = true;
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
