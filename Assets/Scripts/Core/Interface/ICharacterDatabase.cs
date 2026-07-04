using JetBrains.Annotations;
using UnityEngine;

public interface ICharacterDatabase
{
    int CharacterCount { get; }
    CharacterService GetCharacterById(int id);
}
