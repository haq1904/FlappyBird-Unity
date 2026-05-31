
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject Vcam;
    [SerializeField] private float parallaxEffect =1;
    [SerializeField] private float moveSpeed=1;
    [SerializeField] private float xCoordinateToReset = -24;
    private float startPosY;
    private float startPosX;

    private void Awake()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        //Calculate distance for parallax effect
        float distance = Vcam.transform.position.y * parallaxEffect; // 0: won't move , 1: move with camera , 0.5: half;
        //Calculate finalX for moving left by x
        float finalX = transform.position.x - (moveSpeed * Time.fixedDeltaTime);
        //Calculate finalY for moving background with y ( parallax)
        float finalY = startPosY + distance;

        //if background reaches the point, reset position.
        if (transform.position.x < xCoordinateToReset)
            finalX = startPosX;

        transform.position = new Vector3(finalX, finalY, transform.position.z);


    }
}
