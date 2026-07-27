using UnityEngine;

public abstract class ShopItemService : ScriptableObject, IShopItemData
{
    abstract public int Id { get; set; }
    abstract public CharacterService Character { get; set; }
    abstract public float Price { get; set; }
}
