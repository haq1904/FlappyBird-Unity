using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "New shop database", menuName = "Scriptable Objects/Shop/New shop database")]
public class ShopDatabase : ShopDatabaseService
{
    [SerializeField] private List<ShopItemService> _itemList;

    public override int GetItemCount
    {
        get
        {
            return _itemList.Count;
        }
    }

    public override ShopItemService GetShopItemById(int id)
    {
        if (_itemList == null) return null;
        return _itemList.FirstOrDefault(c => c.Id == id);
    }
}
