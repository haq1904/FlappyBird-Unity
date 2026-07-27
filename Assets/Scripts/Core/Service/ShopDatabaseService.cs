using UnityEngine;

public abstract class ShopDatabaseService : MonoBehaviour
{
    public abstract int GetItemCount { get; }
    public abstract ShopItemService GetShopItemById(int id);
}
