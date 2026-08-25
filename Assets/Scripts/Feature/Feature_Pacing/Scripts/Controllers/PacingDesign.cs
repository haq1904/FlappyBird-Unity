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
    [SerializeField] private Warning _warningSign;
    [SerializeField] private float _delayTimeToSpawnCoin = 0.5f;
    [SerializeField] private float _distanceBetweenCoin = 0.5f;
    [SerializeField] private Boolean isTest;
    [SerializeField] private Mode modeIsTest;
    [SerializeField] private TypeMode typeMode;
    //use for spawning coins close with the midle of the pipes
    private float _currPipeHeight;

    private ObjectPoolingService _poolService;

    private Sequence _lastSequence;
    private Sequence _infiniteSpeedSeq;
    private float _infiniteSpeedOffset = 0f;

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

    private void Awake()
    {
        _poolService = FindAnyObjectByType<ObjectPoolingService>();
        if (_poolService == null)
            Debug.Log("Can not get pool service");
    }

    private void OnDisable()
    {
        mainSequence?.Kill();
        _lastSequence?.Kill();
        _infiniteSpeedSeq?.Kill();
        DOTween.Kill("SpawningWarningObstacle");
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
        _infiniteSpeedSeq?.Kill();
        DOTween.Kill("SpawningWarningObstacle");
    }

    public void GameRestart()
    {
        GameOver();
    }

    private void PlayScenarios()
    {
        _infiniteSpeedOffset = 0f;
        mainSequence = DOTween.Sequence();


        if (!isTest)
        {   //Main scenario
            mainSequence.Append(BuildScenario(easyScenario[0], 10));
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
                    mainSequence.Append(BuildScenario(easyScenario[0], -1));
                    break;
                case (Mode.Easy, TypeMode.Two):
                    mainSequence.Append(BuildScenario(easyScenario[1], -1));
                    break;
                case (Mode.Normal, TypeMode.One):
                    mainSequence.Append(BuildScenario(normalScenario[0], -1));
                    break;
                case (Mode.Normal, TypeMode.Two):
                    mainSequence.Append(BuildScenario(normalScenario[1], -1));
                    break;
                case (Mode.Hard, TypeMode.One):
                    mainSequence.Append(BuildScenario(hardScenario[0], -1));
                    break;
                case (Mode.Hard, TypeMode.Two):
                    mainSequence.Append(BuildScenario(hardScenario[1], -1));
                    break;
            }
        }

    }

    private Sequence BuildScenario(PacingScenario currScenario, float duration)
    {
        int loopCount = Mathf.RoundToInt(duration / currScenario.timeToSpawn);//Get number of loop by separate time to spawn with duration
        if (duration < 0)
        {
            loopCount = 9999;
            _infiniteSpeedSeq?.Kill();
            _infiniteSpeedSeq = DOTween.Sequence();
            _infiniteSpeedSeq.AppendInterval(10f);
            _infiniteSpeedSeq.AppendCallback(() =>
            {
                _infiniteSpeedOffset += 2f;
                Debug.Log($"[PacingDesign] Infinite Speed increased! Current offset: {_infiniteSpeedOffset}");
            });
            _infiniteSpeedSeq.SetLoops(-1);
        }

        Sequence mainSequence = DOTween.Sequence();

        SchedulePipeAndCoin(mainSequence, currScenario, loopCount);
        ScheduleWarningObstacle(mainSequence, currScenario, duration, "AttackingBird");
        ScheduleWarningObstacle(mainSequence, currScenario, duration, "Storm");

        return mainSequence;
    }

    private void SchedulePipeAndCoin(Sequence mainSequence, PacingScenario currScenario, int loopCount)
    {
        Sequence pipeAndCoinSeq = DOTween.Sequence();
        float currentPipeSpeed = 2f; // Fallback cho Coin nếu Pipe chưa spawn được

        pipeAndCoinSeq.AppendCallback(() =>
        {
            if (currScenario.TryGetObstacleGroup("Pipe", out ObstacleGroup pipeGroup))
            {
                ObstacleService[] pipeList = pipeGroup.Obstacles;
                ObstacleService pipe = pipeList[UnityEngine.Random.Range(0, pipeList.Length)];

                float baseSpeed = pipeGroup.MoveSpeed + _infiniteSpeedOffset;
                currentPipeSpeed = baseSpeed; // Lưu lại final speed để Coin bay cùng tốc độ với Pipe

                SpawnPipe(pipe, transform.position, Quaternion.identity, baseSpeed);
            }
        });

        pipeAndCoinSeq.AppendInterval(_delayTimeToSpawnCoin);
        pipeAndCoinSeq.AppendCallback(() => SpawnCoin(currentPipeSpeed));

        pipeAndCoinSeq.AppendInterval(currScenario.timeToSpawn - _delayTimeToSpawnCoin);
        pipeAndCoinSeq.SetLoops(loopCount);

        mainSequence.Append(pipeAndCoinSeq);
    }

    private void SpawnPipe(ObstacleService pipe, Vector3 position, Quaternion quaternion, float moveSpeed)
    {
        ObstacleService pipeClone = _poolService.SpawnObject<ObstacleService>(pipe, position, quaternion);
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

    private void ScheduleWarningObstacle(Sequence mainSequence, PacingScenario currScenario, float duration, string groupName)
    {
        if (currScenario.TryGetObstacleGroup(groupName, out ObstacleGroup obstacleGroup))
        {
            ObstacleService[] obstacleList = obstacleGroup.Obstacles;
            float forceToSet = obstacleGroup.ForceMagnitude;
            float radiusToSet = obstacleGroup.Radius;

            if (duration < 0)
            {
                // Chế độ Vô Hạn: Cứ mỗi 6 giây, chọn một thời điểm random trong 0-3 giây để đẻ chướng ngại vật
                Sequence warningObstacleSeq = DOTween.Sequence();
                warningObstacleSeq.AppendCallback(() =>
                {
                    float randDelay = UnityEngine.Random.Range(0f, 3f);
                    DOVirtual.DelayedCall(randDelay, () =>
                    {
                        float currentBaseSpeed = obstacleGroup.MoveSpeed + _infiniteSpeedOffset;
                        Warning signClone = SpawnWarningSign(1);
                        if (signClone.DurationToFollow != 0)
                        {
                            float delay = signClone.DurationToFollow;
                            DOVirtual.DelayedCall(delay + 0.1f, () =>
                            {
                                ObstacleService randObstacle = obstacleList[UnityEngine.Random.Range(0, obstacleList.Length)];
                                float finalSpeedToSet = UnityEngine.Random.Range(currentBaseSpeed, currentBaseSpeed + 2f);
                                SpawnSpecialObstacle(randObstacle, finalSpeedToSet, forceToSet, radiusToSet, signClone);
                            }).SetId("SpawningWarningObstacle").SetLink(gameObject);
                        }
                    }).SetId("SpawningWarningObstacle").SetLink(gameObject);
                });
                warningObstacleSeq.AppendInterval(6f); // Chu kỳ mỗi 6 giây
                warningObstacleSeq.SetLoops(9999);

                // Chèn sequence đẻ chướng ngại vật chạy song song từ giây thứ 0 của mainSequence
                mainSequence.Insert(0, warningObstacleSeq);
            }
            else
            {
                // Chế độ Hữu Hạn: Rải đều 2-5 chướng ngại vật ngẫu nhiên trong khoảng thời gian duration
                float obstacleCount = UnityEngine.Random.Range(2, 5);
                for (int i = 2; i <= obstacleCount; i++)
                {
                    float randSpawnTime = UnityEngine.Random.Range(0f, duration);
                    mainSequence.InsertCallback(randSpawnTime, () =>
                    {
                        float currentBaseSpeed = obstacleGroup.MoveSpeed + _infiniteSpeedOffset;
                        Warning signClone = SpawnWarningSign(1);
                        if (signClone.DurationToFollow != 0)
                        {
                            float delayTimeToSpawnObstacle = signClone.DurationToFollow;
                            DOVirtual.DelayedCall(delayTimeToSpawnObstacle + 0.1f, () =>
                            {
                                ObstacleService randObstacle = obstacleList[UnityEngine.Random.Range(0, obstacleList.Length)];
                                float finalSpeedToSet = UnityEngine.Random.Range(currentBaseSpeed, currentBaseSpeed + 2f);
                                SpawnSpecialObstacle(randObstacle, finalSpeedToSet, forceToSet, radiusToSet, signClone);
                            }).SetId("SpawningWarningObstacle").SetLink(gameObject);
                        }
                        else
                        {
                            Debug.Log("Pacing Design can not get delay time from sign clone.");
                        }
                    });
                }
            }
        }
    }

    private void SpawnSpecialObstacle(ObstacleService specialObstacle, float speedToSet, float forceToSet, float radiusToSet, Warning signClone)
    {
        Vector3 posToSpawn = new Vector3(transform.position.x, signClone.LastPosition.y, transform.position.z);
        ObstacleService specialObstacleClone = Instantiate(specialObstacle, posToSpawn, Quaternion.Euler(0, 0, 20f));
        specialObstacleClone.SetSpeed(speedToSet);
        specialObstacleClone.SetForceMagnitude(forceToSet);
        specialObstacleClone.SetRadius(radiusToSet);
    }

    private Warning SpawnWarningSign(float followSpeed)
    {
        float randSpawnHeight = UnityEngine.Random.Range(-4.2f, 4.2f);

        // Lấy tọa độ trục X của mép Phải màn hình (Viewport x = 1)
        float rightEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;

        // Neo vào mép phải và thụt lùi vào trong màn hình 2 unit (rightEdgeX - 2f)
        Vector3 spawnPos = new Vector3(rightEdgeX - 2f, randSpawnHeight, 0);

        Warning signClone = Instantiate(_warningSign, spawnPos, Quaternion.identity);
        signClone.SetTarget(_mainBird);
        signClone.SetFollowSpeed(followSpeed);
        return signClone;
    }

}


