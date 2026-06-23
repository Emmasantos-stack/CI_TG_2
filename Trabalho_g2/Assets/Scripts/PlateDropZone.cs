using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlateDropZone : MonoBehaviour, IDropHandler
{
    public NutrientsGameManager gameManager;

    public Image pratoImage;

    private Color corOriginal;

    void Start()
    {
        corOriginal = pratoImage.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        FoodItem food = eventData.pointerDrag.GetComponent<FoodItem>();

        if (food == null)
            return;

        if (food.saudavel)
        {
            pratoImage.color = Color.green;

            gameManager.AdicionarPonto();

            food.gameObject.SetActive(false);
        }
        else
        {
            pratoImage.color = Color.red;

            gameManager.RemoverPonto();

            food.VoltarAoInicio();
        }

        Invoke(nameof(ResetarCor), 0.5f);
    }

    void ResetarCor()
    {
        pratoImage.color = corOriginal;
    }
}