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

    private Sequence mainSequence,childSequence;
   
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
        if (!isTest)
        {   //Main scenario
            mainSequence.Append(BuildScenario(easyScenario[0],15));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(normalScenario[0], 25));
            mainSequence.AppendInterval(1f);
            mainSequence.Append(BuildScenario(easyScenario[1], 15));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(hardScenario[0], 20));
            mainSequence.AppendInterval(1f);
            mainSequence.Append(BuildScenario(normalScenario[1], 15));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(hardScenario[1], 20));

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
        int timeToLoop;
         
        //Get time to loop by separate time to spawn with duration
       timeToLoop= Mathf.RoundToInt(duration / currScenario.timeToSpawn);

        childSequence = DOTween.Sequence();
        childSequence.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = currScenario.allowedPipe[UnityEngine.Random.Range(0, currScenario.allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity, currScenario.moveSpeed);
        });
        //Set time to spawn
        childSequence.AppendInterval(currScenario.timeToSpawn);
        childSequence.SetLoops(timeToLoop);

        return childSequence;
    }

    private void SpawnPipe(Obstacle obstacle, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        Obstacle cloneObstacle = Instantiate(obstacle, position, quaternion);
        cloneObstacle.SetSpeed(moveSpeed);
    }

    
}


