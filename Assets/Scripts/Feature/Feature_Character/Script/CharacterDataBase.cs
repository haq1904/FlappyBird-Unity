using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New character database", menuName = "Scriptable Objects/Characters/New character database")]
public class CharacterDataBase : CharacterDataBaseService
{
    [SerializeField] private CharacterService[] _characters;

    public override int CharacterCount
    {
        get { return _characters.Length; }
    }

    public override CharacterService GetCharacterById(int id)
    {
        if (_characters == null) return null;
        return _characters.FirstOrDefault(c => c.Id == id);
    }

   
}
