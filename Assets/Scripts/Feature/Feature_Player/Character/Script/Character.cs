
using UnityEngine;

[CreateAssetMenu(fileName = "New character", menuName = "Scriptable Objects/Characters/New character")]

[System.Serializable]
public class Character : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public Sprite DisplaySprite { get; private set; }
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController AnimController { get; private set; }
}
