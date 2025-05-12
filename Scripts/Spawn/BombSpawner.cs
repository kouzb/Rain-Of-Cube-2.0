using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override Bomb Get(Vector3 position)
    {
        Bomb bomb = base.Get(position);
        return bomb;
    }

    protected override void InitializePool()
    {
        _pool = new ObjectPool<Bomb>(
            CreateInstance,
            bomb => bomb.gameObject.SetActive(true),
            bomb => bomb.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }
}
