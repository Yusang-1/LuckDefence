using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SelectPlatformUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI entityName;

    [SerializeField] private float UIMoveSpeed;

    private IEnumerator openUICoroutine;
    private IEnumerator closeUICoroutine;

    [SerializeField] private int openDirection;
    private bool isOpen;

    private Vector3 startPosition;
    private Vector3 endPosition;

    public bool IsOpen => isOpen;

    public void Initialize()
    {
        gameObject.SetActive(false);

        startPosition = rectTransform.localPosition;
        endPosition = rectTransform.localPosition + openDirection * (Vector3)rectTransform.sizeDelta;                
    }    

    public void OpenUI(Platform platform)
    {
        if(closeUICoroutine != null)
        {
            StopCoroutine(closeUICoroutine);
        }

        entityName.text = platform.Entities[0].Data.EntityName;       
       
        gameObject.SetActive(true);

        openUICoroutine = OpenUIAnimation();
        StartCoroutine(openUICoroutine);
    }

    public void OnCloseUI()
    {
        if(openUICoroutine != null)
        {
            StopCoroutine(openUICoroutine);
        }

        closeUICoroutine = CloseUIAnimation();
        StartCoroutine(closeUICoroutine);
    }

    private IEnumerator OpenUIAnimation()
    {
        float moveDistance = 0;
        float goalDistance = Vector3.Distance(endPosition, rectTransform.localPosition);
        Vector3 position;

        while(moveDistance < goalDistance)
        {
            moveDistance += UIMoveSpeed * Time.deltaTime;

            position = rectTransform.localPosition;
            position.x += UIMoveSpeed * Time.deltaTime * openDirection;

            rectTransform.localPosition = position;
            Debug.Log("open");
            yield return null;
        }

        isOpen = true;
    }

    private IEnumerator CloseUIAnimation()
    {
        isOpen = false;

        float moveDistance = 0;
        float goalDistance = Vector3.Distance(startPosition, rectTransform.localPosition);
        Vector3 position;

        while (moveDistance < goalDistance)
        {
            moveDistance += UIMoveSpeed * Time.deltaTime;

            position = rectTransform.localPosition;
            position.x += UIMoveSpeed * Time.deltaTime * -openDirection;

            rectTransform.localPosition = position;
            Debug.Log("close");
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
