using DG.Tweening;
using System.Linq;
using UnityEngine;

public class PacingDesign : MonoBehaviour
{

    [SerializeField] private PacingScenario[] easyScenario;

    [SerializeField] private PacingScenario[] normalScenario;

    [SerializeField] private PacingScenario[] hardScenario;

    private Sequence activeSequence;
   
    public void OnStartGame()//Receives event from EasyModeManager
    {
        PlayScenarios();
    }

    private void PlayScenarios()
    {
        //Get scenarioes
        PacingScenario randEasyScena = easyScenario[UnityEngine.Random.Range(0, easyScenario.Length)];
        //PacingScenario randNormalScena = normalScenario[UnityEngine.Random.Range(0, normalScenario.Length)];
        PacingScenario randNormalScena = normalScenario[0];
        //PacingScenario randHardScena = hardScenario[UnityEngine.Random.Range(0, hardScenario.Length)];
        PacingScenario randHardScena = hardScenario[0];

        BuildScenario(activeSequence, randEasyScena,20);
        





    }

    private void BuildScenario(Sequence currSequence, PacingScenario currScenario, float duration )
    {
        
        Obstacle obstacle;
        //Get time to loop by separate time to spawn with duration
        int timeToLoop = Mathf.RoundToInt(duration / currScenario.timeToSpawn);

        currSequence = DOTween.Sequence();
        currSequence.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = currScenario.allowedPipe[UnityEngine.Random.Range(0, currScenario.allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity, currScenario.moveSpeed);
        });
        //Set time to spawn
        currSequence.AppendInterval(currScenario.timeToSpawn);
        currSequence.SetLoops(timeToLoop);
    }

    private void SpawnPipe(Obstacle obstacle, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        Obstacle cloneObstacle = Instantiate(obstacle, position, quaternion);
        cloneObstacle.SetSpeed(moveSpeed);
    }
}


