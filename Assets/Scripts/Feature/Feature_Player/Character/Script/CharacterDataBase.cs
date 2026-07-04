using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New character database", menuName = "Scriptable Objects/Characters/New character database")]
public class CharacterDataBase : ScriptableObject
{
    [SerializeField] private Character[] _characters;

    public int CharacterCount
    {
        get { return _characters.Length; }
    }

    public Character GetCharacterById(int id)
    {
        if (_characters == null) return null;
        return _characters.FirstOrDefault(c => c.Id == id);
    }

    
}
