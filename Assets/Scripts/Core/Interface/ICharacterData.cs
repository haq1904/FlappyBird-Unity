using UnityEngine;

public interface ICharacterData
{
    int Id { get; set; }
    Sprite DisplaySprite { get; set; }
    string DisplayName { get; set; }
    RuntimeAnimatorController AnimController { get; set; }
}
