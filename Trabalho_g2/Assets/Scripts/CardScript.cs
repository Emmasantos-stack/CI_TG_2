using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Image Below;
    public Image Cover;

    public void Awake()
    {
        Below.gameObject.SetActive(true);
        Cover.gameObject.SetActive(true);
    }

    // Define cor
    public void SetBelowColor(Color newColor)
    {
        Below.color = newColor;
    }

    // Define imagem
    public void SetBelowImage(Sprite newImage)
    {
        Below.color = Color.white;
        Below.sprite = newImage;
    }

    // Esconde a tampa
    public void DisableCover()
    {
        Cover.gameObject.SetActive(false);
    }

    // Mostra a tampa
    public void EnableCover()
    {
        Cover.gameObject.SetActive(true);
    }

    public int cardID; // O número do par (ex: Abacate = ID 1, Vitamina E = ID 1)

public void SetCardData(Sprite imagem, int id)
{
    Below.sprite = imagem; // Define a imagem (da fruta ou da vitamina)
    cardID = id;           // Guarda a ID para podermos comparar depois
}

}