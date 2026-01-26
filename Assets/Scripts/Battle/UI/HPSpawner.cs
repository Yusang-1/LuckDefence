using UnityEngine;

public class HPSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_HPUI;
    [SerializeField] private Transform poolTransform;

    private HPUI[] hpUIs;

    private void Start()
    {
        
    }

    public void Initialize(StageSO stageData)
    {
        GameObject hpUI;
        int maxEnemyCount = stageData.MaxEnemyCount;
        hpUIs = new HPUI[maxEnemyCount];

        for (int i = 0; i < maxEnemyCount; i++)
        {
            hpUI = Instantiate(m_HPUI, poolTransform);
            hpUI.SetActive(false);

            hpUIs[i] = hpUI.GetComponent<HPUI>();
        }
    }

    public void ActivateHP(Entity entity)
    {
        foreach(HPUI hpUI in hpUIs)
        {
            if(hpUI.IsMatched == false)
            {
                hpUI.matchEntity(entity);
                break;
            }
        }
    }
}
