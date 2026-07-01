using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Linq;
using UnityEngine;

public class PacingDesign : MonoBehaviour
{

    [SerializeField] private PacingScenario[] easyScenario;

    [SerializeField] private PacingScenario[] normalScenario;

    [SerializeField] private PacingScenario[] hardScenario;

    [Header("Fields")]
    [SerializeField] private ItemService _coinPrefab;
    [SerializeField] private GameObject _mainBird;
    [SerializeField] private GameObject _attackingBird;
    [SerializeField] private float _delayTimeToSpawnCoin = 0.5f;
    [SerializeField] private float _distanceBetweenCoin = 0.5f;
    [SerializeField] private Boolean isTest;
    [SerializeField] private Mode modeIsTest;
    [SerializeField] private TypeMode typeMode;

    //use for spawning coins close with the midle of the pipes
    private float _currPipeHeight;

    public enum Mode
    {
        Easy,
        Normal,
        Hard
    }

    public enum TypeMode
    {
        One,
        Two
    }

    private Sequence mainSequence,finalSequence;
   
    public void StartGame()//Receives event from EasyModeManager
    {
        PlayScenarios();
    }

    public void GameOver()
    {
        mainSequence?.Kill();
        finalSequence?.Kill();
    }

    public void GameRestart()
    {
        GameOver();
    }

    private void PlayScenarios()
    {
        mainSequence = DOTween.Sequence();
        CreateFinalSequence();
       
        
        if (!isTest)
        {   //Main scenario
            mainSequence.Append(BuildScenario(easyScenario[0],15));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(normalScenario[0], 25));
            mainSequence.AppendInterval(1f);
            mainSequence.Append(BuildScenario(easyScenario[1], 15));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(hardScenario[0], 30));
            mainSequence.AppendInterval(1f);
            mainSequence.Append(BuildScenario(normalScenario[1], 10));
            mainSequence.AppendInterval(3.5f);
            mainSequence.AppendCallback(() =>
            {
                finalSequence.Restart();
            });

        }
        else
        {
            switch (modeIsTest,typeMode)
            {
                case (Mode.Easy,TypeMode.One):
                    mainSequence.Append(BuildScenario(easyScenario[0], 30));
                    break;
                case (Mode.Easy, TypeMode.Two):
                    mainSequence.Append(BuildScenario(easyScenario[1], 30));
                    break;
                case (Mode.Normal, TypeMode.One):
                    mainSequence.Append(BuildScenario(normalScenario[0], 30));
                    break;
                case (Mode.Normal, TypeMode.Two):
                    mainSequence.Append(BuildScenario(normalScenario[1], 30));
                    break;
                case (Mode.Hard, TypeMode.One):
                    mainSequence.Append(BuildScenario(hardScenario[0], 30));
                    break;
                case (Mode.Hard, TypeMode.Two):
                    mainSequence.Append(BuildScenario(hardScenario[1], 30));
                    break;
            }
        }

    }

    private Sequence BuildScenario(PacingScenario currScenario, float duration )
    {
        
        ObstacleService obstacle;
        int loopCount = Mathf.RoundToInt(duration / currScenario.timeToSpawn);//Get number of loop by separate time to spawn with duration

        Sequence mainSequence = DOTween.Sequence();

        Sequence pipeAndCoinSeq = DOTween.Sequence();

        pipeAndCoinSeq.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = currScenario.allowedPipe[UnityEngine.Random.Range(0, currScenario.allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity, currScenario.moveSpeed);           
        });
        pipeAndCoinSeq.AppendInterval(_delayTimeToSpawnCoin);
        pipeAndCoinSeq.AppendCallback(() => SpawnCoin(currScenario.moveSpeed));
        //Set time to spawn
        // With currScenario.timeToSpawn - _delayTimeToSpawnCoin, we keep exactly the time to spawn the next pipe.
        pipeAndCoinSeq.AppendInterval(currScenario.timeToSpawn - _delayTimeToSpawnCoin);
        pipeAndCoinSeq.SetLoops(loopCount);

        mainSequence.Append(pipeAndCoinSeq);

        if (duration >= 10 )//&& UnityEngine.Random.value < 0.5f)
        {
            Sequence AttackingBirdSeq = DOTween.Sequence();
            float birdCount = UnityEngine.Random.Range(2, 5);
            Debug.Log("Amount of bird : " + birdCount);
            for (int i = 2; i <= birdCount; i++)
            {
                float randSpawnTime = UnityEngine.Random.Range(0f, duration);
                mainSequence.InsertCallback(randSpawnTime, () =>
                {
                    SpawnAttackingBird(currScenario.moveSpeed + 2);
                    Debug.Log("Bird is spawned");
                });
            }
        }
        return mainSequence;
    }

    private void CreateFinalSequence()
    {
        ObstacleService obstacle;
        finalSequence = DOTween.Sequence();
        finalSequence.Pause();
        finalSequence.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = hardScenario[1].allowedPipe[UnityEngine.Random.Range(0, hardScenario[1].allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity, hardScenario[1].moveSpeed);
        });
        //Set time to spawn
        finalSequence.AppendInterval(hardScenario[1].timeToSpawn);
        finalSequence.SetLoops(-1);
    }

    private void SpawnPipe(ObstacleService obstacle, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        ObstacleService cloneObstacle = Instantiate(obstacle, position, quaternion);
        cloneObstacle.SetSpeed(moveSpeed);
        _currPipeHeight = cloneObstacle.GetSpawnHeight();
    }

    private void SpawnCoin(float speedToSet)
    {
        float randCoinHeight = UnityEngine.Random.Range(_currPipeHeight - 1.6f, _currPipeHeight + 1.6f);
        int randQuantity = UnityEngine.Random.Range(0, 6);
        for (int i = 0; i < randQuantity;i++) 
        {
            //use x value of PacingDesign to place coin with the same distance(by i).
            float calculatedPosX = transform.position.x + i * _distanceBetweenCoin;
            Vector3 finalEachCoinTrans = new Vector3(calculatedPosX + _distanceBetweenCoin, randCoinHeight, 0);
            ItemService coinClone = Instantiate(_coinPrefab, finalEachCoinTrans, Quaternion.identity);
            coinClone.SetSpeed(speedToSet);
        }
    }

    private void SpawnAttackingBird(float speedToSet)
    {
        Vector3 mainBirdPos = new Vector3(transform.position.x, _mainBird.transform.position.y, transform.position.z);
        GameObject attackingBirdClone = Instantiate(_attackingBird, mainBirdPos, Quaternion.identity);
        Debug.Log(attackingBirdClone.transform.position);  
    }

    
}


