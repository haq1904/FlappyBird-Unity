using UnityEngine;

public abstract class CharacterDataBaseService : ScriptableObject,ICharacterDatabase
{
    public abstract int CharacterCount{get;}

    public abstract CharacterService GetCharacterById(int id);

    
}
