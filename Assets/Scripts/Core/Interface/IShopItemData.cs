using UnityEngine;

public interface IShopItemData
{
    int Id { get; set; }
    CharacterService Character { get; set; }
    float Price { get; set; }
}
