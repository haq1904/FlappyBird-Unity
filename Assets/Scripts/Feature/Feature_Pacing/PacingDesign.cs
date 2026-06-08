using DG.Tweening;
using System.Linq;
using UnityEngine;

public class PacingDesign : MonoBehaviour
{

    [SerializeField] private PacingScenario[] easyScenario;

    [SerializeField] private PacingScenario[] normalScenario;

    [SerializeField] private PacingScenario[] hardScenario;

    private Sequence easySequence;
    private Sequence normalSequence;
    private Sequence hardSequence;
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
        PacingScenario randHardScena = hardScenario[UnityEngine.Random.Range(0, hardScenario.Length)];

        Obstacle obstacle;

        easySequence = DOTween.Sequence();
        easySequence.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = randEasyScena.allowedPipe[UnityEngine.Random.Range(0, randEasyScena.allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity, randEasyScena.moveSpeed);
        });
        //Set time to spawn
        easySequence.AppendInterval(randEasyScena.timeToSpawn);
        easySequence.SetLoops(6);




    }

    private void SpawnPipe(Obstacle obstacle, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        Obstacle cloneObstacle = Instantiate(obstacle, position, quaternion);
        cloneObstacle.SetSpeed(moveSpeed);
    }
}


