using UnityEngine;

public class InfoSpawner : MonoBehaviour
{
    public delegate void StateInfo(float totalSpawned, float countAll, float countActive);
    public event StateInfo CountStateUpdate;

    protected void InvokeStateUpdate(float totalSpawned, float countAll, float countActive)
    {
        CountStateUpdate?.Invoke(totalSpawned, countAll, countActive);
    }
}
