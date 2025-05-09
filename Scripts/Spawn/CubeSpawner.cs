using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private Transform _spawnPoint;

    public event Action<Cube> CubeReleased;

    private bool _isSpawning = true;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {   
        StartCoroutine(ContinuousSpawning());
    }

    private void Release(Cube cube) 
    {
        cube.Released -= HandleCubeReleased;
        CubeReleased?.Invoke(cube);
        _pool.Release(cube);
    }

    protected override void InitializePool()
    {
        _pool = new Pool<Cube>(
            CreateInstance,
            Prepare,
            cube => cube.gameObject.SetActive(false),
            _poolCapacity,
            _poolMaxSize
            );
    }

    private void Prepare(Cube cube)
    {
        cube.transform.position = _spawnPoint.position;
        cube.gameObject.SetActive(true);
        cube.InitVelocity(Vector3.up);
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
        _bombSpawner.Get(cube.transform.position);
        cube.gameObject.SetActive(false);
    }
}