using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarAlimento : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 posicaoInicial;

    [HideInInspector] public Transform paiOriginal;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        // Adiciona um CanvasGroup se não existir (ajuda a ignorar o raio do rato ao largar)
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posicaoInicial = rectTransform.anchoredPosition;
        paiOriginal = transform.parent;
        
        // Bloqueia os raios do rato para que a zona onde vais largar consiga detetar o clique
        canvasGroup.blocksRaycasts = false;
        
        // Opcional: faz com que fique ligeiramente transparente ao arrastar
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move o alimento seguindo o rato/toque
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Se não foi largado num local correto (o pai não mudou), volta para o início
        if (transform.parent == paiOriginal)
        {
            rectTransform.anchoredPosition = posicaoInicial;
        }
    }
}