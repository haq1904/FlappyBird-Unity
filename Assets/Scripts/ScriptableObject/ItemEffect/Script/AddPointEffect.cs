using UnityEngine;

[CreateAssetMenu(fileName = "Add Point Effect",menuName = "Scriptable Objects/Item Effects/Add Point Effect")]
public class AddPointEffect : ItemEffectStrategy, IEffect
{
    [SerializeField] private float pointToAdd = 1;
    public override void ApplyEffect(IReceivable gameObject)
    {
        gameObject.AddPoint(pointToAdd);
    }
}
