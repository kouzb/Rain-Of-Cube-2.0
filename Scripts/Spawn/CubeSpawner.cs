using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private Transform _spawnPoint;

    public event Action<Cube> CubeReleased;

    private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(ContinuousSpawning());
    }

    protected override void Release(Cube cube) 
    {
        cube.Released -= HandleCubeReleased;
        Pool.Release(cube);
    }

    protected override void InitializePool()
    {
        Pool = new ObjectPool<Cube>(
            CreateInstance,
            Prepare,
            cube => cube.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaxSize
            );
    }

    private void Prepare(Cube cube)
    {
        cube.transform.position = _spawnPoint.position;
        cube.gameObject.SetActive(true);
        cube.Released += HandleCubeReleased;
    }

    private IEnumerator ContinuousSpawning()
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
        Release(cube);
        CubeReleased?.Invoke(cube);
        _bombSpawner.Get(cube.transform.position);
    }
}