using System;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private BasePipe basicPipe;   
    [SerializeField] private BasePipe pipeToTest;
    [SerializeField] private bool isTest = true; 
    [SerializeField] private float timeToSpawn=2;

    private float remainingTime=0;
    private BasePipe currPipe;

    private void OnEnable()
    {
        if (isTest)
        {
            currPipe = pipeToTest;
            return;
        }
        currPipe = basicPipe;

    }
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
        Instantiate(currPipe, transform.position,Quaternion.identity);
    }
}
