using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarAlimento : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 posicaoInicial;
    private Canvas canvasPrincipal; // Guardamos o canvas aqui de forma segura

    [HideInInspector] public Transform paiOriginal;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Procura pelo Canvas subindo na árvore de objetos, sem ir para a câmara
        canvasPrincipal = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posicaoInicial = rectTransform.anchoredPosition;
        paiOriginal = transform.parent;
        
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Se encontramos o Canvas, usamos o scaleFactor dele
        if (canvasPrincipal != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvasPrincipal.scaleFactor;
        }
        else
        {
            // Código de segurança caso ele não encontre o canvas por alguma razão
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == paiOriginal)
        {
            rectTransform.anchoredPosition = posicaoInicial;
        }
    }
}