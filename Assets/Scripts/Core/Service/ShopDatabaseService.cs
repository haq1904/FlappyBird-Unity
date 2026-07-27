using UnityEngine;

public abstract class ShopDatabaseService : ScriptableObject
{
    public abstract int GetItemCount { get; }
    public abstract ShopItemService GetShopItemById(int id);
}
