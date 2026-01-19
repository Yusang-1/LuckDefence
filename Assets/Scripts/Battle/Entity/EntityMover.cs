using UnityEngine;

public abstract class EntityMover : MonoBehaviour
{
    protected Entity entity;    
    protected Vector3 directionVector;    

    public virtual void Initialize(Entity entity)
    {
        this.entity = entity;        
    }

    protected abstract Vector3 GetDestinationVector();
    

    protected abstract void Move();
}
