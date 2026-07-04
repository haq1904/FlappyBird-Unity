
using UnityEngine;

[CreateAssetMenu(fileName = "New character", menuName = "Scriptable Objects/Characters/New character")]

[System.Serializable]
public class Character : CharacterService
{
    [field: SerializeField] public override int Id { get; set; }
    [field: SerializeField] public override Sprite DisplaySprite { get; set; }
    [field: SerializeField] public override string DisplayName { get; set; }
    [field: SerializeField] public override RuntimeAnimatorController AnimController { get; set; }
}
