using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected int pooledNum;
    //protected Entity[] Entities;
    protected Dictionary<int, Entity> EntityDict;

    public virtual void Initialize(Dictionary<int, Entity> entityDict)
    {

    }

    public virtual void ActiveEntity(int code, Platform platform)
    {
                
    }

    public virtual void ActiveEntity()
    {

    }

    public virtual void DeactiveEntity(int index)
    {

    }    
}
