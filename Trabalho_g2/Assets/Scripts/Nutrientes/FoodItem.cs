using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool saudavel = true;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 posicaoInicial;
    private Outline outline;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        outline = GetComponent<Outline>();
        if (outline == null)
            outline = gameObject.AddComponent<Outline>();

        if (saudavel)
            outline.effectColor = Color.green;
        else
            outline.effectColor = Color.red;

        outline.effectDistance = new Vector2(4, -4);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posicaoInicial = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void VoltarAoInicio()
    {
        rectTransform.anchoredPosition = posicaoInicial;
    }

    public void Desaparecer()
    {
        gameObject.SetActive(false);
    }
}