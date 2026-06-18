using UnityEngine;

public abstract class Obstacle : MonoBehaviour, IObstacle
{
    abstract public void SetSpeed(float moveSpeed);
}
