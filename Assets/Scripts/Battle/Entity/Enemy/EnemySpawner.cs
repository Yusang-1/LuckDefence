using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private HPSpawner hpSpawner;
    [SerializeField] private Transform spawnArea;

    [SerializeField] private BattleDataSO battleData;

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
        }

        StartCoroutine(ActiveEnemyCoroutine(entities, roundData.SpawnDelay));
    }

    private IEnumerator ActiveEnemyCoroutine(Entity[] entities, float spawnDelay)
    {
        WaitForSeconds waitSpawnDelay = new WaitForSeconds(spawnDelay);
        for(int i = 0; i < entities.Length; i++)
        {
            entities[i].gameObject.SetActive(true);
            entities[i].EntityActivated();

            battleData.CurrentEnemyCount++;

            hpSpawner.ActivateHP(entities[i]);

            yield return waitSpawnDelay;
        }
    }
}
