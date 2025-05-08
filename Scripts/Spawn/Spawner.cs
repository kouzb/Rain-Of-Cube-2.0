using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolCapacity = 5;
    [SerializeField] protected int _poolMaxSize = 5;

    protected Pool<T> _pool;
    protected int _totalCreated;
    protected int _totalActive;

    public event Action Created;

    public int TotalCreated => _totalCreated;
    public int TotalActive => _totalActive;

    protected virtual void Awake()
    {
        InitializePool();
    }

    protected abstract void InitializePool();

    protected virtual T CreateInstance()
    {
        T instance = Instantiate(_prefab);
        return instance;
    }

    public virtual T Get(Vector3 position)
    {
        T instance = _pool.Get();
        _totalCreated++;
        Created?.Invoke();
        instance.transform.position = position; 
        instance.gameObject.SetActive(true);
        return instance;
    }
}
