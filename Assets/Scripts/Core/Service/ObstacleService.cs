using System.Runtime.InteropServices;
using UnityEngine;

public abstract class ObstacleService : MonoBehaviour, IObstacle
{
    abstract public void SetSpeed(float moveSpeed);

    abstract public float GetSpawnHeight();
}
