using TMPro;
using UnityEngine;

public class SpawnerStatsUI : MonoBehaviour
{
    [SerializeField] private Spawner<Bomb> _bombSpawner;
    [SerializeField] private Spawner<Cube> _cubeSpawner;
    [SerializeField] private TMP_Text _cubeStatsText;
    [SerializeField] private TMP_Text _bombStatsText;

    private string _cubeTitle = "cube";
    private string _bombTitle = "bomb";

    private void OnEnable()
    {
        _bombSpawner.CountStateUpdate += (t, a, c) => ShowStats(_bombStatsText, _bombTitle, t, a, c);
        _cubeSpawner.CountStateUpdate += (t, a, c) => ShowStats(_cubeStatsText, _cubeTitle, t, a, c); 
    }

    private void ShowStats(TMP_Text targetText, string title, float totalSpawned, float allCreated, float countActive)
    {
        targetText.text = title + " \n Заспавнено :" + totalSpawned + "\nСоздано :" + allCreated + 
            "\nАктивных на сцене: " + countActive;
    }

    private void OnDisable()
    {
        _bombSpawner.CountStateUpdate -= (t, a, c) => ShowStats(_bombStatsText, _bombTitle, t, a, c);
        _cubeSpawner.CountStateUpdate -= (t, a, c) => ShowStats(_cubeStatsText, _cubeTitle, t, a, c);
    }
}
