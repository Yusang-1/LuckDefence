using UnityEngine;

public class Entity : MonoBehaviour
{
    public void EntityActivated()
    {
        Debug.Log($"{gameObject.name} activated");
    }
}
