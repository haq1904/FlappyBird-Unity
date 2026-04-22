using UnityEngine;

public class GroundWall : MonoBehaviour, IDeadly
{
    public DeathType GetDeathType() => DeathType.GroundHit;
}

