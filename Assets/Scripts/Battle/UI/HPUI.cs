using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField] RectTransform hpTransform;
    [SerializeField] Vector3 upVector;

    private Entity entity;
    private IDamagable damagableEntity;

    private bool isMatched;

    public bool IsMatched => isMatched;

    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(entity.transform.position + upVector);
    }

    public void matchEntity(Entity entity)
    {        
        this.entity = entity;
        damagableEntity = entity as IDamagable;

        damagableEntity.HPChanged += OnSetUI;

        isMatched = true;
    }

    public void OnSetUI(int hp)
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        float value = hp / entity.Data.MaxMp;
        hpTransform.localScale = new Vector3(value, 1, 1);

        if(value <= 0)
        {
            ResetUI();

            gameObject.SetActive(false);
        }
    }

    private void ResetUI()
    {
        damagableEntity.HPChanged -= OnSetUI;

        damagableEntity = null;

        isMatched = false;
    }
}
