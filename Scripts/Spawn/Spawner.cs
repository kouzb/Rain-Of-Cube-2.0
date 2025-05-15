using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int PoolCapacity = 5;
    [SerializeField] protected int PoolMaxSize = 10;

    protected ObjectPool<T> Pool;

    private int _totalCreated;
    private int _totalSpawned;

    public int TotalCreated => _totalCreated;
    public int TotalSpawned => _totalSpawned;
    public int ActiveCount => Pool.CountActive;

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected abstract void InitializePool();

    public virtual T Get(Vector3 position)
    {
        T instance = Pool.Get();
        instance.transform.position = position; 
        instance.gameObject.SetActive(true);
        _totalSpawned++;
        CheckCreatedObj();
        return instance;
    }

    protected virtual T CreateInstance()
    {
        T instance = Instantiate(Prefab);
        return instance;
    }

    protected virtual void Release(T spawnedObject)
    {
        Pool.Release(spawnedObject);
    }

    private void CheckCreatedObj()
    {
        if (_totalCreated <= PoolMaxSize)
        {
            _totalCreated++; 
        }
    }
}