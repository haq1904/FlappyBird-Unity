using NUnit.Framework;
using System.IO.Pipes;
using UnityEditor.Animations;
using UnityEngine;
using System.Collections.Generic;

public class PipeSpawnerScirpt : MonoBehaviour
{
    public GameObject pipe;
    public float timer;
    public float spawnRate = 2;
    public float heightOffset = 50;
    private List<GameObject> activePipes = new List<GameObject>();
    private bool isGamePause=false;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        PipeMoveScript.OnMoveToDeadZone += InActivePipe;
        LogicManager.OnGamePause += PausePipes;
      
        LogicManager.OnGameRestart += RestartPipes;
    }

    private void OnDisable()
    {
        PipeMoveScript.OnMoveToDeadZone -= InActivePipe;
        LogicManager.OnGamePause -= PausePipes;
        
        LogicManager.OnGameRestart -= RestartPipes;
    }
        

    private void StartPipes(LogicManager.GameState state)
    {
        
        isGamePause = false;
        if (activePipes.Count > 0)
        {
            foreach (GameObject obj in activePipes)
            {
                obj.GetComponent<PipeMoveScript>().enabled = true;
            }
        }
        else
        {
            SpawnPipe();
            timer = 0; 
        }
    }

    private void PausePipes(LogicManager.GameState state)
    {
        isGamePause = true;
        if(activePipes.Count > 0)
        {
            foreach ( GameObject obj in activePipes)
            {
                obj.GetComponent<PipeMoveScript>().enabled = false;
            }
        }
    }

   private void RestartPipes(LogicManager.GameState state)
    {
        isGamePause = false;
        if (activePipes.Count > 0)
        {
            activePipes.RemoveAll(obj =>
            {
                ObjectPoolingManager.ReturnObjectToPool(obj);
                return true;
            });
            Debug.Log("All pipes have been cleared");
        }
    }

    private void InActivePipe(GameObject obj)
    {
        activePipes.Remove(obj);
        ObjectPoolingManager.ReturnObjectToPool(obj);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate && !isGamePause)
        {
            timer += Time.deltaTime;
        }
        else if(!isGamePause)
        {
            SpawnPipe();
            timer = 0;
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        GameObject obj = ObjectPoolingManager.SpawnObject(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint)), transform.rotation, ObjectPoolingManager.PoolType.GameObject);
        activePipes.Add(obj);
        obj.GetComponent<PipeMoveScript>().enabled = true;
    }
}
