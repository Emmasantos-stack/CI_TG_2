using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlateDropZone : MonoBehaviour, IDropHandler
{
    public NutrientsGameManager gameManager;
    public Image pratoImage;

    public Color corNormal = Color.white;
    public Color corCerto = Color.green;
    public Color corErrado = Color.red;

    public void OnDrop(PointerEventData eventData)
    {
        FoodItem food = eventData.pointerDrag.GetComponent<FoodItem>();

        if (food == null) return;

        if (food.saudavel)
        {
            pratoImage.color = corCerto;
            gameManager.AdicionarPonto();

            // alimento certo fica no prato
            food.transform.SetParent(transform);
            food.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            pratoImage.color = corErrado;
            gameManager.RemoverPonto();

            // alimento errado volta ao sítio
            food.VoltarAoInicio();
        }

        Invoke(nameof(ResetarCor), 1f);
    }

    void ResetarCor()
    {
        pratoImage.color = corNormal;
    }
}