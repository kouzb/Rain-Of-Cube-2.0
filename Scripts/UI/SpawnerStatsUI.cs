using TMPro;
using UnityEngine;

public class SpawnerStatsUI : MonoBehaviour
{
    [SerializeField] private Spawner<Bomb> _bombSpawner;
    [SerializeField] private Spawner<Cube> _cubeSpawner;

    [SerializeField] protected TMP_Text _cubeStatsText;

    [SerializeField] private TMP_Text _bombStatsText;

    private void Update()
    {
        ShowStatsCube();
        ShowStatsBomb();
    }

    private void ShowStatsCube()
    {
        _cubeStatsText.text = "���� \n ���������� :" + _cubeSpawner.TotalSpawned + "\n������� :" + _cubeSpawner.TotalCreated + 
            "\n�������� �� �����: " + _cubeSpawner.ActiveCount;
    }

    private void ShowStatsBomb()
    {
        _bombStatsText.text = "����� \n ���������� :" + _bombSpawner.TotalSpawned + "\n������� :" + _bombSpawner.TotalCreated +
            "\n�������� �� �����: " + _bombSpawner.ActiveCount;
    }
}
