using System;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private BasicPipe basicPipe;
    [SerializeField] private OneWayMovePipe oneWayMovePipe;
    [SerializeField] private TwoWayMovePipe twoWayMovePipe;
    [SerializeField] private float timeToSpawn=2;

    private float remainingTime=0;
    private void Update()
    {
        if (remainingTime < timeToSpawn)
        {
            remainingTime += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            remainingTime = 0;
        }
    }

    private void SpawnPipe()
    {
        Instantiate(twoWayMovePipe, transform.position,Quaternion.identity);
    }
}
