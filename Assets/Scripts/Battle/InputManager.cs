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

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<IHoldableObject>(out IHoldableObject holdable))
            {
                if (holdable == m_ISelectable)
                {
                    holdable.Holded();
                }
            }
        }

        if (context.canceled && isHold)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            RaycastHit2D hit2D = Physics2D.Raycast(vec, Vector3.forward, float.MaxValue);

            if (hit2D && hit2D.collider.gameObject.TryGetComponent<IHoldableObject>(out IHoldableObject holdable))
            {
                holdable.HoldReleased();
            }
            isHold = false;
        }
    }
}
