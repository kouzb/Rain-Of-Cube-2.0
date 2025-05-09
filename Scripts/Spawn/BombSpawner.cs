using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Explosion _explosion;

    public event Action BombExploded;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _explosion.Initialize(_pool);
    }

    public override Bomb Get(Vector3 position)
    {
        Bomb bomb = base.Get(position);
        bomb.Exploded += Explode;
        return bomb;
    }

    protected override void InitializePool()
    {
        _pool = new Pool<Bomb>(
            CreateInstance,
            bomb => bomb.gameObject.SetActive(true),
            bomb => bomb.gameObject.SetActive(false),
            _poolCapacity,
            _poolMaxSize
            );
    }

    private void Explode(Bomb bomb)
    {
        _explosion.Explode(bomb);
        bomb.Exploded -= Explode;
        BombExploded?.Invoke();
    }
}
