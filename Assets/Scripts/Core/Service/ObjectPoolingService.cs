using UnityEngine;

public enum PoolType
{
    ParticleSystem,
    GameObject,
    SoundFX
}

public abstract class ObjectPoolingService : MonoBehaviour
{
    public abstract T SpawnObject<T>(T typePrefab, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObject) where T : Component;
    public abstract GameObject SpawnObject(GameObject typePrefab, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObject);
    public abstract void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObject);
}
