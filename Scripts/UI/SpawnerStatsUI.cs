using TMPro;
using UnityEngine;

public abstract class SpawnerStatsUI : MonoBehaviour
{
    [SerializeField] private Spawner<Bomb> _bombSpawner;

    [SerializeField] private TMP_Text _cubeStatsText;
    [SerializeField] private TMP_Text _bombStatsText;

    protected string _title;


    private void OnEnable()
    {
        //_bombSpawner.CountStateUpdate += (t, a, c) => ShowStats(_bombStatsText, _bombTitle, t, a, c);
        
    }

    protected void ShowStats(TMP_Text targetText, string title, float totalSpawned, float allCreated, float countActive)
    {
        targetText.text = title + " \n Заспавнено :" + totalSpawned + "\nСоздано :" + allCreated + 
            "\nАктивных на сцене: " + countActive;
    }

    private void OnDisable()
    {
        //_bombSpawner.CountStateUpdate -= (t, a, c) => ShowStats(_bombStatsText, _bombTitle, t, a, c);
    }
}
