using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : InfoSpawner where T: MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int PoolCapacity = 5;
    [SerializeField] protected int PoolMaxSize = 10;

    protected ObjectPool<T> Pool;
    private int _totalSpawned;

    private int _allCreated => Pool.CountAll;
    private int _activeCount => Pool.CountActive;

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
        InvokeStateUpdate(_totalSpawned, _allCreated, _activeCount);
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
        InvokeStateUpdate(_totalSpawned, _allCreated, _activeCount);
    }
}