using System;
using Unity.VisualScripting;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20;
    private float deadZone = -70;
    public static event Action<GameObject> OnMoveToDeadZone;


    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if( transform.position.x <= deadZone)
        {
            OnMoveToDeadZone?.Invoke(gameObject);
        }
        
    }


    
}
