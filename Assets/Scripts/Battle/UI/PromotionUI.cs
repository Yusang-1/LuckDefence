using UnityEngine;
using UnityEngine.UI;

public class PromotionUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Promotion promotion;

    private void Start()
    {
        button.enabled = false;
        promotion.PromotionableChanged += OnButtonAvailable;
    }

    private void OnDisable()
    {
        promotion.PromotionableChanged -= OnButtonAvailable;
    }

    public void OnPromotion()
    {
        promotion.OnPromotion();
    }

    public void OnButtonAvailable(bool value)
    {
        button.enabled = value;
    }
}
