using UnityEngine;
[CreateAssetMenu(fileName = "Add Coin", menuName = "Scriptable Objects/Item Effects/Add Coin")]
public class AddCoin : ItemEffect
{
    [SerializeField] private float _coinToAdd = 1;
    public override void ApplyEffect(IReceivable gameObject)
    {
        gameObject.AddCoin(_coinToAdd);
    }

}
