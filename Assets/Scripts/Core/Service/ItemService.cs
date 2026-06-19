using System;
using UnityEngine;

public abstract class ItemService : MonoBehaviour, IItem
{
    [SerializeField] protected EffectService effect;

    abstract public void ApplyEffect(IReceivable gameObj);

}
