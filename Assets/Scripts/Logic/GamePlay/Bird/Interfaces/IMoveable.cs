using UnityEngine;

public interface IMoveable 
{
    void Flap();
    void ResetVelocity();
    float Force { get; set; }
    Rigidbody2D RB { get; set; }
}
