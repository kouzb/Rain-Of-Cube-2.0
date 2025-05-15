using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    public override Bomb Get(Vector3 position)
    {
        Bomb bomb = base.Get(position);
        bomb.Exploded += Release;
        return bomb;
    }

    protected override void InitializePool()
    {
        Pool = new ObjectPool<Bomb>(
            CreateInstance,
            bomb => bomb.gameObject.SetActive(true),
            actionOnRelease: OnReleaseBomb,
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaxSize
            );
    }

    private void OnReleaseBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
        bomb.Exploded -= Release;
    }
}
