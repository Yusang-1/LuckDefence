using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void EntityActivated()
    {
        Debug.Log($"{gameObject.name} activated");
    }
}
