using UnityEngine;

[CreateAssetMenu(fileName = "AddPointEffect",menuName = "ScriptableObject/Items")]
public class AddPointEffect : ItemEffectStrategy
{
    [SerializeField] private float pointToAdd = 1;
    public override void ApplyEffect(IReceivable gameObject)
    {
        gameObject.AddPoint(pointToAdd);
    }
}
