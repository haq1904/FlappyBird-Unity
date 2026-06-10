using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class PacingDesign : MonoBehaviour
{

    [SerializeField] private PacingScenario[] easyScenario;

    [SerializeField] private PacingScenario[] normalScenario;

    [SerializeField] private PacingScenario[] hardScenario;

    [SerializeField] private Boolean isTest;

    [SerializeField] private Mode modeIsTest;

    [SerializeField] private TypeMode typeMode;
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

    private Sequence mainSequence,childSequence,finalSequence;
   
    public void OnStartGame()//Receives event from EasyModeManager
    {
        PlayScenarios();
    }

    public void OnGameOver()
    {
        mainSequence.Kill();
        childSequence.Kill();
    }

    private void PlayScenarios()
    {
        mainSequence = DOTween.Sequence();

        finalSequence = DOTween.Sequence();
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
                    mainSequence.Append(BuildScenario(normalScenario[0], 30));
                    break;
                case (Mode.Hard, TypeMode.Two):
                    mainSequence.Append(BuildScenario(normalScenario[1], 30));
                    break;
            }
        }

    }

    private Sequence BuildScenario(PacingScenario currScenario, float duration )
    {
        
        Obstacle obstacle;
        int loopCount;
        if (duration == -1)
            loopCount = -1;//for infinity loop
        else
            loopCount = Mathf.RoundToInt(duration / currScenario.timeToSpawn);//Get number of loop by separate time to spawn with duration

        childSequence = DOTween.Sequence();
        childSequence.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = currScenario.allowedPipe[UnityEngine.Random.Range(0, currScenario.allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity, currScenario.moveSpeed);
        });
        //Set time to spawn
        childSequence.AppendInterval(currScenario.timeToSpawn);
        childSequence.SetLoops(loopCount);

        return childSequence;
    }

    private void CreateFinalSequence()
    {
        Obstacle obstacle;
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

    private void SpawnPipe(Obstacle obstacle, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        Obstacle cloneObstacle = Instantiate(obstacle, position, quaternion);
        cloneObstacle.SetSpeed(moveSpeed);
    }

    
}


