using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntitySO data;
    [SerializeField] private EntityMover mover;

    public EntitySO Data => data;

    private void Start()
    {
        mover.Initialize(this);
    }
    
    public virtual void EntityActivated()
    {
        Debug.Log($"{gameObject.name} activated");
    }
}
