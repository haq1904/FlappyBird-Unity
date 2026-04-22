using UnityEngine;

public class CeilingWall : MonoBehaviour, IDeadly
{
    public DeathType GetDeathType() => DeathType.CeilingHit;
}
