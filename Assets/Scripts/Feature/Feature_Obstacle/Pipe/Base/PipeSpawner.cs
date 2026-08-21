using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private ObstacleService _obstaclePrefab;
    [SerializeField] private float _timeToSpawn = 2f;

    [Header("Test Settings")]
    [SerializeField] private float _moveSpeedToTest = 5f;
    [SerializeField] private float _forceMagnitudeToTest = -100f;

    private float _remainingTime = 0f;

    private void OnEnable()
    {
        SpawnObstacle();
    }

    private void Update()
    {
        if (_remainingTime < _timeToSpawn)
        {
            _remainingTime += Time.deltaTime;
        }
        else
        {
            SpawnObstacle();
            _remainingTime = 0f;
        }
    }

    private void SpawnObstacle()
    {
        if (_obstaclePrefab != null)
        {
            ObstacleService obstacle = Instantiate(_obstaclePrefab, transform.position, Quaternion.identity);
            obstacle.SetSpeed(_moveSpeedToTest);
            obstacle.SetForceMagnitude(_forceMagnitudeToTest);
        }
    }

    public void StopSpawning()
    {
        enabled = false;
    }

    public void HandleRestart()
    {
        _remainingTime = 0f;
        SpawnObstacle(); // Đẻ ra chướng ngại vật đầu tiên luôn cho nóng
    }
}
