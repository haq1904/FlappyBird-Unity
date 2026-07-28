using System.Collections.Generic;
using UnityEngine;

public abstract class ShopDatabaseService : ScriptableObject
{
    public abstract List<ShopItemService> ItemList { get; }
    public abstract int GetItemCount { get; }
    public abstract ShopItemService GetShopItemById(int id);
}
