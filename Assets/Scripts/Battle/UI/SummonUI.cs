using UnityEngine;
using UnityEngine.UI;

public class SummonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private CharacterSpawner spawner;

    private void Start()
    {
        spawner = FindFirstObjectByType<CharacterSpawner>();
    }

    public void Initialize()
    {
        spawner = FindFirstObjectByType<CharacterSpawner>();
    }

    public void OnSummon()
    {
        spawner.SpawnEntity();
    }
}
