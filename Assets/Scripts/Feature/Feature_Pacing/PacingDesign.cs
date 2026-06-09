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
    public enum Mode
    {
        Easy,
        Normal,
        Hard
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
        //Get scenarioes
        PacingScenario randEasyScena = easyScenario[UnityEngine.Random.Range(0, easyScenario.Length)];
        //PacingScenario randNormalScena = normalScenario[UnityEngine.Random.Range(0, normalScenario.Length)];
        PacingScenario randNormalScena = normalScenario[0];
        //PacingScenario randHardScena = hardScenario[UnityEngine.Random.Range(0, hardScenario.Length)];
        PacingScenario randHardScena = hardScenario[0];
        mainSequence = DOTween.Sequence();
        if (!isTest)
        {           
            mainSequence.Append(BuildScenario(randEasyScena, 30));
            mainSequence.AppendInterval(2f);
            mainSequence.Append(BuildScenario(randHardScena, 20));
        }
        else
        {
            switch (modeIsTest)
            {
                case Mode.Easy:
                    mainSequence.Append(BuildScenario(randEasyScena, 30));
                    break;
                case Mode.Normal:
                    mainSequence.Append(BuildScenario(randNormalScena, 30));
                    break;
                case Mode.Hard:
                    mainSequence.Append(BuildScenario(randHardScena, 30));
                    break;
            }
        }

    }

    private Sequence BuildScenario(PacingScenario currScenario, float duration )
    {
        
        Obstacle obstacle;
        //Get time to loop by separate time to spawn with duration
        int timeToLoop = Mathf.RoundToInt(duration / currScenario.timeToSpawn);

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


