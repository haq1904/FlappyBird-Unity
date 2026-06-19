using System;
using UnityEngine;

public abstract class ItemService : MonoBehaviour, IItem
{
    [SerializeField] protected EffectService _effect;
    [SerializeField] protected float _speed = 0; 

    abstract public void ApplyEffect(IReceivable gameObj);

    abstract public void SetSpeed(float speed);

}
