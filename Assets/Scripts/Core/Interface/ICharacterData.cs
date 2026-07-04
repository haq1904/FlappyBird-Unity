using UnityEngine;

public interface ICharacterData
{
    int Id { get;}
    Sprite DisplaySprite { get;}
    string DisplayName { get;}
    RuntimeAnimatorController AnimController { get;}
}
