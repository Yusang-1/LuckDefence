using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntitySO data;
    [SerializeField] private EntityMover mover;

    public EntitySO Data => data;
    public EntityMover Mover => mover;

    private void Awake()
    {
        mover?.Initialize(this);
    }
    
    public virtual void EntityActivated()
    {
        //Debug.Log($"{gameObject.name} activated");
    }
}
