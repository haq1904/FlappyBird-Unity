
using DG.Tweening;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private GameObject Vcam;
    [SerializeField] private float parallaxEffect = 1;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float xCoordinateToReset = -24;

    [Header("Move speed follows pacing")]
    [SerializeField] private float speed1 = 1;
    [SerializeField] private float speed2 = 1;
    [SerializeField] private float speed3 = 1;
    [SerializeField] private float speed4 = 1;
    [SerializeField] private float speed5 = 1;
    [SerializeField] private float speed6 = 1;



    private float startPosY;
    private float startPosX;
    private Sequence mainSequence;


    private void Awake()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    private void OnEnable()
    {
        HandleChangeMoveSpeed();
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

    public void HandleRestart()
    {
        HandleChangeMoveSpeed();
    }


    public void HandleGameOver()
    {
        moveSpeed = 0;
        mainSequence?.Kill();
    }

    private void HandleChangeMoveSpeed()
    {
        mainSequence?.Kill();
        mainSequence = DOTween.Sequence();
        mainSequence.AppendCallback(() => moveSpeed = speed1);
        mainSequence.AppendInterval(10f);
        mainSequence.AppendCallback(() => moveSpeed = speed2);
        mainSequence.AppendInterval(18.5f);
        mainSequence.AppendCallback(() => moveSpeed = speed3);
        mainSequence.AppendInterval(12f);
        mainSequence.AppendCallback(() => moveSpeed = speed4);
        mainSequence.AppendInterval(18.5f);
        mainSequence.AppendCallback(() => moveSpeed = speed5);
        mainSequence.AppendInterval(12f);
        mainSequence.AppendCallback(() => moveSpeed = speed6);

    }
}
