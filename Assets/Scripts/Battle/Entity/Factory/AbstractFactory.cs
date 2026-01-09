using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected int pooledNum;
    protected Entity[] Entities;

    public virtual void Initialize(Entity[] entities)
    {

    }

    public virtual void ActiveEntity(Entity entity, Platform platform)
    {
                
    }

    public virtual void DeactiveEntity(int index)
    {

    }    
}
