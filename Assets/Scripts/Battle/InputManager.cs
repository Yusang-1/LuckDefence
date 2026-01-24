using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ISelectableObject m_ISelectable;
    private Vector3 mousePosition;
    private bool isHold;

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_ISelectable?.SelectedEnd();

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

        if (isHold) return;

        if (context.canceled)
        {
            Debug.Log(1);
            ISelectableObject selectable;
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out selectable))
            {
                selectable.Selected();
            }
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHold = true;

            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                if (selectable == m_ISelectable)
                {
                    m_ISelectable.Holded();
                }
            }
        }

        if (context.canceled && isHold)
        {
            Debug.Log(2);
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<ISelectableObject>(out ISelectableObject selectable))
            {
                selectable.HoldReleased();
            }
            isHold = false;
        }
    }
}
