using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;    
    }

    void Update()
    {
        transform.Translate(Vector3.left*Time.deltaTime * moveSpeed);
        if (transform.position.x < -24)
            transform.position = startPos ;
    }
}
