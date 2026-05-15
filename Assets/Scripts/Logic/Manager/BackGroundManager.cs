using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject Vcam;
    [SerializeField] private float parallaxEffect;
    private float startPos;

    private void Awake()
    {
        startPos = transform.position.y;
    }

    private void FixedUpdate()
    {
        float distance = Vcam.transform.position.y * parallaxEffect; // 0: won't move , 1: move with camera , 0.5: half;
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
    }
}
