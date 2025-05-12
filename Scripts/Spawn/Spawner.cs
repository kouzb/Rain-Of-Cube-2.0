using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolCapacity = 5;
    [SerializeField] protected int _poolMaxSize = 10;

    protected ObjectPool<T> _pool;
    protected int _totalCreated;
    protected int _totalSpawned;

    public event Action Created;

    public int TotalCreated => _totalCreated;
    public int TotalSpawned => _totalSpawned;
    public int ActiveCount => _pool.CountActive;

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected abstract void InitializePool();

    public virtual T Get(Vector3 position)
    {
        T instance = _pool.Get();
        Created?.Invoke();
        instance.transform.position = position; 
        instance.gameObject.SetActive(true);
        _totalSpawned++;
        CheckCreatedObj();
        return instance;
    }

    protected virtual T CreateInstance()
    {
        T instance = Instantiate(_prefab);
        return instance;
    }

    private void CheckCreatedObj()
    {
        if (_totalCreated <= _poolMaxSize)
        {
            _totalCreated++;
        }
    }
}