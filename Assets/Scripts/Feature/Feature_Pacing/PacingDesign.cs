using DG.Tweening;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Serialization;
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
        PacingScenario randEasyScena = easyScenario[UnityEngine.Random.Range(0, easyScenario.Count())];
        PacingScenario randNormalScena = easyScenario[UnityEngine.Random.Range(0, normalScenario.Count())];
        PacingScenario randHardScena = easyScenario[UnityEngine.Random.Range(0, hardScenario.Count())];

        GameObject obstacle;

        easySequence = DOTween.Sequence();
        easySequence.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            obstacle = randEasyScena.allowedPipe[UnityEngine.Random.Range(0, randEasyScena.allowedPipe.Count())];
            SpawnPipe(obstacle, transform.position, Quaternion.identity);
        });
        //Set time to spawn
        easySequence.AppendInterval(randEasyScena.timeToSpawn);
        easySequence.SetLoops(10);

        
    }

    private void SpawnPipe(GameObject obstacle, Vector3 position, Quaternion quaternion)
    {
        Instantiate(obstacle, position, quaternion);
    }
}


