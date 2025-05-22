using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerUI : SpawnerStatsUI
{
    [SerializeField] private Spawner<Cube> _cubeSpawner;

    private void OnEnable()
    {
        //_cubeSpawner.CountStateUpdate += ShowStats(_cubeStatsText, _title, t, a, c);
    }

    private void OnDisable()
    {
        //_cubeSpawner.CountStateUpdate -= ShowStats(_cubeStatsText, _title, t, a, c);
    }
}
