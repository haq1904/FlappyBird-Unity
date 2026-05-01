using UnityEngine;

[CreateAssetMenu(fileName = "ItemEffectStrategy", menuName = "ScriptableObject/Items")]
public abstract class ItemEffectStrategy : ScriptableObject
{
    public abstract void ApplyEffect(IReceivable gameObject);
}
