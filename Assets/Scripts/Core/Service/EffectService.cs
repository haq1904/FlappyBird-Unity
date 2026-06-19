using UnityEngine;

public abstract class EffectService : ScriptableObject, IEffect
{
    abstract public void ApplyEffect(IReceivable gameObj);
}
