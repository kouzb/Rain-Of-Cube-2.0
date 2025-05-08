using TMPro;
using UnityEngine;

public class SpawnerUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI CountCreatedObjectText;
    [SerializeField] protected TextMeshProUGUI CountActiveObjectText;
    [SerializeField] protected string NameObject;

    private void Start()
    {
        ShowInfo(0, 0);
    }

    protected void ShowInfo(int countCreated, int countActive)
    {
        CountCreatedObjectText.text = "�������� " + NameObject + " :" + countCreated.ToString();
        CountActiveObjectText.text = "������� " + NameObject + " :" + countActive.ToString();
    }
}
