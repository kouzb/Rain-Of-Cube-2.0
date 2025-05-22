using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private Transform _spawnPoint;

    private bool _isSpawning = true;

    public event Action<Cube> CubeReleased;

    private void Start()
    {
        StartCoroutine(SpawningCoroutine());
    }

    protected override void InitializePool() { }

    protected override Cube Get(Vector3 position)
    {
        Cube cube = base.Get(position);
        cube.transform.position = _spawnPoint.position;
        cube.Released += HandleCubeReleased;
        return cube;
    }

    protected override void OnRelease(Cube cube) 
    {
        CubeReleased?.Invoke(cube);
        cube.Released -= HandleCubeReleased;
        _pool.Release(cube);
        //_bombSpawner.Get(cube.transform.position);
    }

    private IEnumerator SpawningCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_repeatRate);
        
        while (_isSpawning)
        {
            Get(_spawnPoint.position);
            yield return wait;
        }
    }

    private void HandleCubeReleased(Cube cube)
    {
        OnRelease(cube);
        
        
    }
}