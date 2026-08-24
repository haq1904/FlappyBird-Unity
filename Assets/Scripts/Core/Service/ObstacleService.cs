using System.Runtime.InteropServices;
using UnityEngine;

public abstract class ObstacleService : MonoBehaviour, IObstacle
{
    abstract public void SetSpeed(float moveSpeed);

    public virtual void SetForceMagnitude(float force) { }

    public virtual void SetRadius(float value) { }

    public virtual float GetSpawnHeight() { return 0; }

}
