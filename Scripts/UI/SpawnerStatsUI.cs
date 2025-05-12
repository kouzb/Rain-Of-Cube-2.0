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
        _cubeStatsText.text = "Кубы \n Заспавнено :" + _cubeSpawner.TotalSpawned + "\nСоздано :" + _cubeSpawner.TotalCreated + 
            "\nАктивных на сцене: " + _cubeSpawner.ActiveCount;
    }

    private void ShowStatsBomb()
    {
        _bombStatsText.text = "Бомбы \n Заспавнено :" + _bombSpawner.TotalSpawned + "\nСоздано :" + _bombSpawner.TotalCreated +
            "\nАктивных на сцене: " + _bombSpawner.ActiveCount;
    }
}
