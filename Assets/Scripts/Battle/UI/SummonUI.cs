using UnityEngine;
using UnityEngine.UI;

public class SummonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private CharacterSpawner spawner;

    public void OnSummon()
    {
        spawner.SpawnEntity();
    }
}
