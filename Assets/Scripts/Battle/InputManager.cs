using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector3 mousePosition;
    private ISelectableObject m_ISelectable;

    public void OnSelect(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            mousePosition = context.ReadValue<Vector2>();

            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                m_ISelectable = selectable;
            }
        }

        if(context.performed)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));

            //Debug.DrawRay(vec - Vector3.forward * 10, Vector3.forward * float.MaxValue, Color.red, 3f);
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                selectable.Selected();
            }
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                if(selectable == m_ISelectable)
                {                    
                    selectable.Holded();
                }
            }
        }            
    }
}
