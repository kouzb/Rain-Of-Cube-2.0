using System;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> where T : MonoBehaviour
{
    private ObjectPool<T> _pool;
    private int _activeObj;

    public int ActiveObj => _activeObj;

    public Pool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, int defaultCapacity = 5, int maxSize = 15)
    {
        _pool = new ObjectPool<T>(
        createFunc,
        actionOnGet: actionOnGet,
        actionOnRelease: actionOnRelease,
        collectionCheck: true,
        defaultCapacity: defaultCapacity,
        maxSize: maxSize);
    }

    public T Get()
    {
        _activeObj++;
        return _pool.Get();
    }

    public void Release(T obj)
    {
        _activeObj--;
        _pool.Release(obj);
    }
}
