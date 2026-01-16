using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnArea;

    public void Instantiate()
    {

    }

    public void SpawnEnemy(RoundData roundData)
    {
        GameObject go;
        Entity[] entities = new Entity[roundData.EnemyCount];

        for (int i = 0; i < roundData.EnemyCount; i++)
        {
            go = Instantiate(roundData.Enemy.gameObject, spawnArea.position, Quaternion.identity);
            go.SetActive(false);
            entities[i] = go.GetComponent<Entity>();
            //entities[i] = roundData.Enemy; //이게 같은건가
        }

        StartCoroutine(ActiveEnemyCoroutine(entities, roundData.SpawnDelay));
    }

    private IEnumerator ActiveEnemyCoroutine(Entity[] entities, float spawnDelay)
    {
        WaitForSecondsRealtime waitSpawnDelay = new WaitForSecondsRealtime(spawnDelay);
        for(int i = 0; i < entities.Length; i++)
        {
            entities[i].gameObject.SetActive(true);
            entities[i].EntityActivated();

            yield return waitSpawnDelay;
        }
    }
}
