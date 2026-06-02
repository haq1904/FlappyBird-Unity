using UnityEngine;

public abstract class ItemEffect : ScriptableObject, IEffect
{
    public abstract void ApplyEffect(IReceivable gameObject);
}
