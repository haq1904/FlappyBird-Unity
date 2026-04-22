using UnityEngine;

public enum DeathType
{
    PipeHit,
    GroundHit,
    CeilingHit,
}
public interface IDeadly
{
    public DeathType GetDeathType();
}
