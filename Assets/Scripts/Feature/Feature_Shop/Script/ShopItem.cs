using UnityEngine;

[System.Serializable]
public class ShopItem : ShopItemService
{
    [field: SerializeField] public override int Id { get; set; }
    [field: SerializeField] public override CharacterService Character { get; set; }
    [field: SerializeField] public override float Price { get; set; }
}
