using UnityEngine;

public class AddCoin : ItemEffect
{
    [SerializeField] private float _coinToAdd = 1;
    public override void ApplyEffect(IReceivable gameObject)
    {
    }
}
