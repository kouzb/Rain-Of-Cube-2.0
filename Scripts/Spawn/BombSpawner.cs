using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    protected override void InitializePool() { }

    protected override Bomb Get(Vector3 position)
    {
        Bomb bomb = base.Get(position);
        bomb.Exploded += OnRelease;
        return bomb;
    }

    protected override void OnRelease(Bomb bomb)
    {
        bomb.Exploded -= OnRelease;
        _pool.Release(bomb);
    }
}
