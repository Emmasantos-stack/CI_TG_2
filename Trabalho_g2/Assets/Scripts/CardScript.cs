using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int cardID; // O número do par (ex: Abacate = ID 1, Vitamina E = ID 1)
    
    public Image Below;
    public Image Cover;

    private void Awake()
    {
        // Garante que tudo começa visível quando o jogo inicia
        if (Below != null) Below.enabled = true;
        if (Cover != null) Cover.enabled = true;
    }

    // Define os dados da carta enviados pelo CardsManager
    public void SetCardData(Sprite imagem, int id)
    {
        cardID = id;
        if (Below != null)
        {
            Below.sprite = imagem; // Define a imagem (da fruta ou da vitamina)
            Below.color = Color.white; // Garante que a imagem não fica escura/transparente
        }
    }

    // Define apenas a cor (se precisares para outros testes)
    public void SetBelowColor(Color newColor)
    {
        if (Below != null) Below.color = newColor;
    }

    // Esconde apenas o componente de Imagem da tampa, mantendo o Botão CLICÁVEL!
    public void DisableCover()
    {
        if (Cover != null)
        {
            Cover.enabled = false;
        }
    }

    // Mostra a imagem da tampa novamente
    public void EnableCover()
    {
        if (Cover != null)
        {
            Cover.enabled = true;
        }
    }
}