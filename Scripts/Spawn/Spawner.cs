using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : InfoSpawner where T: MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10;

    protected ObjectPool<T> _pool;
    private int _totalSpawned;

    private int AllCreated => _pool.CountAll;
    private int ActiveCount => _pool.CountActive;

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected abstract void InitializePool();

    protected virtual T Get(Vector3 position)
    {
        T instance = _pool.Get();
        instance.transform.position = position; 
        instance.gameObject.SetActive(true);
        _totalSpawned++;
        InvokeStateUpdate(_totalSpawned, AllCreated, ActiveCount);
        return instance;
    }

    protected virtual void OnRelease(T spawnedObject)
    {
        _pool.Release(spawnedObject);
        spawnedObject.gameObject.SetActive(false);
        InvokeStateUpdate(_totalSpawned, AllCreated, ActiveCount);
    }

    protected virtual int GetPoolCapacity()
    {
        return _poolCapacity;
    }

    protected virtual int GetPoolMaxSize()
    {
        return _poolMaxSize;
    }
}