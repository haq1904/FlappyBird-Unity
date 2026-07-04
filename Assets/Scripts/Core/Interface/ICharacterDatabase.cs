using JetBrains.Annotations;
using UnityEngine;

public interface ICharacterDatabase
{
    int CharacterCount { get; }
    ICharacterData GetCharacterById(int id);
}
