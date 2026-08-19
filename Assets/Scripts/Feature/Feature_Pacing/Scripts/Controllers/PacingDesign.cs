using System;
using System.Linq;
using System.Runtime.CompilerServices;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class PacingDesign : MonoBehaviour
{

    [SerializeField] private PacingScenario[] easyScenario;

    [SerializeField] private PacingScenario[] normalScenario;

    [SerializeField] private PacingScenario[] hardScenario;

    [Header("Fields")]
    [SerializeField] private ItemService _coinPrefab;
    [SerializeField] private PlayerService _mainBird;
    [SerializeField] private ObstacleService _attackingBird;
    [SerializeField] private Warning _warningSign;
    [SerializeField] private float _delayTimeToSpawnCoin = 0.5f;
    [SerializeField] private float _distanceBetweenCoin = 0.5f;
    [SerializeField] private Boolean isTest;
    [SerializeField] private Mode modeIsTest;
    [SerializeField] private TypeMode typeMode;

    //use for spawning coins close with the midle of the pipes
    private float _currPipeHeight;

    private Sequence _lastSequence;

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

    private void OnDisable()
    {
        mainSequence?.Kill();
        _lastSequence?.Kill();
        DOTween.Kill("SpawningBird");
    }

    private Sequence mainSequence;

    public void StartGame()//Receives event from EasyModeManager
    {
        PlayScenarios();
    }

    public void GameOver()
    {
        mainSequence?.Kill();
        _lastSequence?.Kill();
        DOTween.Kill("SpawningBird");
    }

    public void GameRestart()
    {
        GameOver();
    }

    private void PlayScenarios()
    {
        mainSequence = DOTween.Sequence();


        if (!isTest)
        {   //Main scenario
            mainSequence.Append(BuildScenario(easyScenario[0], 15));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(normalScenario[0], 15));
            mainSequence.AppendInterval(2f);
            mainSequence.Append(BuildScenario(easyScenario[1], 10));
            mainSequence.AppendInterval(3.5f);
            mainSequence.Append(BuildScenario(hardScenario[0], 15));
            mainSequence.AppendInterval(2f);
            mainSequence.Append(BuildScenario(normalScenario[1], 10));
            mainSequence.AppendInterval(3.5f).OnComplete(() =>
            {
                _lastSequence = BuildScenario(hardScenario[1], -1);
            });


        }
        else
        {
            switch (modeIsTest, typeMode)
            {
                case (Mode.Easy, TypeMode.One):
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

    private Sequence BuildScenario(PacingScenario currScenario, float duration)
    {

        int loopCount = Mathf.RoundToInt(duration / currScenario.timeToSpawn);//Get number of loop by separate time to spawn with duration
        if (duration < 0)
            loopCount = 9999;

        Sequence mainSequence = DOTween.Sequence();
        Sequence pipeAndCoinSeq = DOTween.Sequence();


        //Creat sequence for pipe and coin to append into main sequence
        pipeAndCoinSeq.AppendCallback(() =>
        {
            //get random allowed pipe from scenario
            if (currScenario.TryGetObstacleList("Pipe", out ObstacleService[] pipeList))
            {
                ObstacleService pipe = pipeList[UnityEngine.Random.Range(0, pipeList.Length)];
                SpawnPipe(pipe, transform.position, Quaternion.identity, currScenario.moveSpeed);
            }
        });
        pipeAndCoinSeq.AppendInterval(_delayTimeToSpawnCoin);
        pipeAndCoinSeq.AppendCallback(() => SpawnCoin(currScenario.moveSpeed));
        //Set time to spawn
        // With currScenario.timeToSpawn - _delayTimeToSpawnCoin, we keep exactly the time to spawn the next pipe.
        pipeAndCoinSeq.AppendInterval(currScenario.timeToSpawn - _delayTimeToSpawnCoin);
        pipeAndCoinSeq.SetLoops(loopCount);

        mainSequence.Append(pipeAndCoinSeq);


        //insert action(spawn Warning sign or AttakingBird) to main sequence
        if (currScenario.TryGetObstacleList("AttackingBird", out ObstacleService[] attackingBirdList))
        {
            if (duration < 0)
            {
                // Chế độ Vô Hạn: Cứ mỗi 6 giây, chọn một thời điểm random trong 0-3 giây để đẻ chim
                Sequence attackingBirdSeq = DOTween.Sequence();
                attackingBirdSeq.AppendCallback(() =>
                {
                    float randDelay = UnityEngine.Random.Range(0f, 3f);
                    DOVirtual.DelayedCall(randDelay, () =>
                    {
                        Warning signClone = SpawnWarningSign(currScenario.moveSpeed - 5);
                        if (signClone.DurationToFollow != 0)
                        {
                            float delay = signClone.DurationToFollow;
                            DOVirtual.DelayedCall(delay + 0.1f, () =>
                            {
                                ObstacleService randBird = attackingBirdList[UnityEngine.Random.Range(0, attackingBirdList.Length)];
                                SpawnAttackingBird(randBird, currScenario.moveSpeed + 6, signClone);
                            }).SetId("SpawningBird").SetLink(gameObject);
                        }
                    }).SetId("SpawningBird").SetLink(gameObject);
                });
                attackingBirdSeq.AppendInterval(6f); // Chu kỳ mỗi 6 giây
                attackingBirdSeq.SetLoops(9999);

                // Chèn sequence đẻ chim chạy song song từ giây thứ 0 của mainSequence
                mainSequence.Insert(0, attackingBirdSeq);
            }
            else
            {
                // Chế độ Hữu Hạn: Rải đều 2-5 con chim ngẫu nhiên trong khoảng thời gian duration
                float birdCount = UnityEngine.Random.Range(2, 5);
                for (int i = 2; i <= birdCount; i++)
                {
                    float randSpawnTime = UnityEngine.Random.Range(0f, duration);
                    mainSequence.InsertCallback(randSpawnTime, () =>
                    {
                        Warning signClone = SpawnWarningSign(currScenario.moveSpeed - 5);
                        if (signClone.DurationToFollow != 0)
                        {
                            float delayTimeToSpawnAttackingBird = signClone.DurationToFollow;
                            DOVirtual.DelayedCall(delayTimeToSpawnAttackingBird + 0.1f, () =>
                            {
                                ObstacleService randAttackingBird = attackingBirdList[UnityEngine.Random.Range(0, attackingBirdList.Length)];
                                SpawnAttackingBird(randAttackingBird, currScenario.moveSpeed + 6, signClone);
                            }).SetId("SpawningBird").SetLink(gameObject);
                        }
                        else
                        {
                            Debug.Log("Pacing Design can not get delay time from sign clone.");
                        }
                    });
                }
            }
        }
        return mainSequence;
    }

    private void SpawnPipe(ObstacleService pipe, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        ObstacleService pipeClone = Instantiate(pipe, position, quaternion);
        pipeClone.SetSpeed(moveSpeed);
        _currPipeHeight = pipeClone.GetSpawnHeight();
    }

    private void SpawnCoin(float speedToSet)
    {
        float randCoinHeight = UnityEngine.Random.Range(_currPipeHeight - 1.6f, _currPipeHeight + 1.6f);
        int randQuantity = UnityEngine.Random.Range(0, 6);
        for (int i = 0; i < randQuantity; i++)
        {
            //use x value of PacingDesign to place coin with the same distance(by i).
            float calculatedPosX = transform.position.x + i * _distanceBetweenCoin;
            Vector3 finalEachCoinTrans = new Vector3(calculatedPosX + _distanceBetweenCoin, randCoinHeight, 0);
            ItemService coinClone = Instantiate(_coinPrefab, finalEachCoinTrans, Quaternion.identity);
            coinClone.SetSpeed(speedToSet);
        }
    }

    private void SpawnAttackingBird(ObstacleService attackingBird, float speedToSet, Warning signClone)
    {
        Vector3 posToSpawn = new Vector3(transform.position.x, signClone.LastPosition.y, transform.position.z);
        ObstacleService attackingBirdClone = Instantiate(attackingBird, posToSpawn, Quaternion.Euler(0, 0, 20f));
        attackingBirdClone.SetSpeed(speedToSet);
    }

    private Warning SpawnWarningSign(float followSpeed)
    {
        float randSpawnHeight = UnityEngine.Random.Range(-4.2f, 4.2f);
        Warning signClone = Instantiate(_warningSign, new Vector3(7, randSpawnHeight, 0), Quaternion.identity);
        signClone.SetTarget(_mainBird);
        signClone.SetFollowSpeed(followSpeed);
        return signClone;
    }

}


