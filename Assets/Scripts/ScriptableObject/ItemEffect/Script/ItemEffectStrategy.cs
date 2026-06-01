using UnityEngine;

public abstract class ItemEffectStrategy : ScriptableObject
{
    public abstract void ApplyEffect(IReceivable gameObject);
}
