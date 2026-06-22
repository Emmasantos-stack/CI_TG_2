using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleHover = 1.15f;
    public float speed = 8f;
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(1f, 1f, 0.7f);

    private Vector3 originalScale;
    private Vector3 targetScale;
    private Image image;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
        image = GetComponent<Image>();
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleHover;

        if (image != null)
            image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;

        if (image != null)
            image.color = normalColor;
    }
}