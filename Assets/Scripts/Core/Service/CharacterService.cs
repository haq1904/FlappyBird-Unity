using UnityEngine;

public abstract class CharacterService : ScriptableObject, ICharacterData
{
    public abstract int Id { get; set; }

    public abstract Sprite DisplaySprite { get; set; }

    public abstract string DisplayName { get; set; }

    public abstract RuntimeAnimatorController AnimController { get; set; }
}
