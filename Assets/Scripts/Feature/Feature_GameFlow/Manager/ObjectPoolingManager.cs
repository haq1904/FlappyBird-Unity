
using System;
using System.Collections.Generic;
using NUnit.Framework.Api;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPoolingManager : MonoBehaviour
{

    [SerializeField] private bool _addToDontDestroyOnLoad = false;

    private GameObject _emptyHolder;
    private static GameObject _particleSystemEmpty;
    private static GameObject _gameObjectEmpty;
    private static GameObject _soundFXEmpty;

    private static Dictionary<GameObject, ObjectPool<GameObject>> _objectPools;
    private static Dictionary<GameObject, GameObject> _cloneToPrefabMap;

    private void Awake()
    {
        _objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        _cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        SetupEmpties();
    }

    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        SoundFX
    }

    public static PoolType poolType;

    private void SetupEmpties()
    {
        _emptyHolder = new GameObject("Object Pools");

        _particleSystemEmpty = new GameObject("Particle Systems");
        _particleSystemEmpty.transform.SetParent(_emptyHolder.transform);

        _gameObjectEmpty = new GameObject("Game objects");
        _gameObjectEmpty.transform.SetParent(_emptyHolder.transform);

        _soundFXEmpty = new GameObject("Sound Effects");
        _soundFXEmpty.transform.SetParent(_emptyHolder.transform);

        if (_addToDontDestroyOnLoad)
            DontDestroyOnLoad(_particleSystemEmpty.transform.root);
    }

    private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(prefab, pos, rot, poolType),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
            );

        _objectPools.Add(prefab, pool);
    }

    private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab, pos, rot);

        prefab.SetActive(true);

        GameObject parentObject = SetParentObject(poolType);
        obj.transform.SetParent(parentObject.transform);

        return obj;
    }

    private static void OnGetObject(GameObject obj)
    {
        //optional get
    }

    private static void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);

    }

    private static void OnDestroyObject(GameObject obj)
    {
        if (_cloneToPrefabMap.ContainsKey(obj))
            _cloneToPrefabMap.Remove(obj);
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemEmpty;
            case PoolType.GameObject:
                return _gameObjectEmpty;
            case PoolType.SoundFX:
                return _soundFXEmpty;
            default:
                return null;
        }
    }

    private static T SpawnObject<T>(GameObject objToSpawn, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObject) where T : UnityEngine.Object
    {
        if (!_objectPools.ContainsKey(objToSpawn))
            CreatePool(objToSpawn, spawnPos, spawnRot, poolType);

        GameObject obj = _objectPools[objToSpawn].Get();

        if (obj != null)
        {
            if (!_cloneToPrefabMap.ContainsKey(obj))
                _cloneToPrefabMap.Add(obj, objToSpawn);

            obj.transform.position = spawnPos;
            obj.transform.rotation = spawnRot;
            obj.SetActive(true);

            if (typeof(T) == typeof(GameObject))
                return obj as T;

            T component = obj.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"The object {objToSpawn.name} doesn't have component type of {typeof(T)} ");
                return null;
            }
            return component;
        }
        return null;

    }

    public static T SpawnObject<T>(T typePrefab, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObject) where T : Component
    {
        return SpawnObject<T>(typePrefab, spawnPos, spawnRot, poolType);
    }

    public static GameObject SpawnObject(GameObject typePrefab, Vector3 spawnPos, Quaternion spawnRot, PoolType poolType = PoolType.GameObject)
    {
        return SpawnObject<GameObject>(typePrefab, spawnPos, spawnRot, poolType);
    }

    public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.GameObject)
    {
        if (_cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
        {
            GameObject parentObject = SetParentObject(poolType);
            if (obj.transform.parent != parentObject)
            {
                obj.transform.SetParent(parentObject.transform);
            }
            if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(obj);
            }
        }
        else
        {
            Debug.LogWarning($"Trying to return an object that is not pooled : {obj.name}");
        }
    }
}
