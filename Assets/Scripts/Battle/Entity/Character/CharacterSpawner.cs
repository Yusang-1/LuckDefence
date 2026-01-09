using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private AbstractFactory[] factories;

    public void SpawnEntity()
    {
        factories[0].GetChar();
    }
}
