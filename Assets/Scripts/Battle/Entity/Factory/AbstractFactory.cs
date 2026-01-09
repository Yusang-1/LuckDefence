using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected Entity[] Entities;
    [SerializeField] private int pooledNum;

    public virtual void GetChar()
    {
        int index = Random.Range(0, Entities.Length-1);

        Instantiate(Entities[index].gameObject); 
    }
}
