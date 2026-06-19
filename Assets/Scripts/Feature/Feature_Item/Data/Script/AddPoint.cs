using UnityEngine;

[CreateAssetMenu(fileName = "Add Point",menuName = "Scriptable Objects/Item Effects/Add Point")]
public class AddPointEffect : EffectService
{
    [SerializeField] private float pointToAdd = 1;
    public override void ApplyEffect(IReceivable gameObject)
    {
        gameObject.AddPoint(pointToAdd);
    }
}
