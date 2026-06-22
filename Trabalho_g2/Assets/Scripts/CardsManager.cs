using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    private List<CardScript> listOfCards; // O total de cartas na cena (ex: 8 cartas para 4 pares)

    [Header("Imagens do Jogo de Memória")]
    [SerializeField]
    private List<Sprite> spritesFrutas;    // Coloca aqui as fotos das frutas (Tamanho ex: 4)

    [SerializeField]
    private List<Sprite> spritesVitaminas; // Coloca aqui as fotos das vitaminas NA MESMA ORDEM (Tamanho ex: 4)

    [SerializeField]
    private AudioSource victoryMusic;

    [SerializeField]
    private TimerScript timerScript;

    [SerializeField] 
    private CanvasGroup canvasGroup; 

    private CardScript firstSelectedItem;
    private CardScript secondSelectedItem;

    private int numberOfMatches = 0;

    void Start()
    {
        // Garante que o número de cartas na cena é o dobro do número de frutas/vitaminas
        if (spritesFrutas.Count != spritesVitaminas.Count || listOfCards.Count != spritesFrutas.Count * 2)
        {
            throw new ApplicationException("Erro: O número de cartas na cena tem de ser o dobro do número de frutas na lista!");
        }

        // Distribuir os pares pelas cartas
        // Cada par (i) vai receber a mesma ID, ligando a Fruta à sua Vitamina
        int cardIndex = 0;
        for (int i = 0; i < spritesFrutas.Count; i++)
        {
            // Configura a carta da Fruta (ID = i)
            listOfCards[cardIndex].SetCardData(spritesFrutas[i], i);
            cardIndex++;

            // Configura a carta da Vitamina correspondente (ID = i)
            listOfCards[cardIndex].SetCardData(spritesVitaminas[i], i);
            cardIndex++;
        }

        // Misturar todas as cartas na mesa para o jogador não saber onde estão
        Shuffle(listOfCards);
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        for (int i = 0; i < listOfCards.Count; i++)
        {
            listOfCards[i].transform.SetSiblingIndex(i);
        }
    }

    public void OnCardClick()
    {
        if (EventSystem.current.currentSelectedGameObject == null) return;
        if (firstSelectedItem && secondSelectedItem) return;

        var clickedItem = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CardScript>();

        // Evita que o jogador clique na mesma carta duas vezes
        if (clickedItem == firstSelectedItem) return;

        if (!firstSelectedItem)
        {
            firstSelectedItem = clickedItem;
            firstSelectedItem.DisableCover();
        }
        else
        {
            secondSelectedItem = clickedItem;
            secondSelectedItem.DisableCover();

            CompareChosenItems();
        }
    }

    // --- A MÁGICA ACONTECE AQUI ---
    private void CompareChosenItems()
    {
        // Em vez de comparar os sprites, comparamos as IDs que demos no Start()!
        if (firstSelectedItem.cardID == secondSelectedItem.cardID)
        {
            // É um par correto! (Ex: Clicou no Abacate [ID 0] e na Vitamina E [ID 0])
            numberOfMatches++;
            StartCoroutine(ResetAndCheckFinish(0, false));
        }
        else
        {
            // Errou o par!
            StartCoroutine(ResetAndCheckFinish(1.5f, true));
        }
    }

    IEnumerator ResetAndCheckFinish(float numberOfSecondsToWait, bool shouldReset)
    {
        if (canvasGroup != null) canvasGroup.interactable = false;

        yield return new WaitForSeconds(numberOfSecondsToWait);

        if (shouldReset)
        {
            firstSelectedItem.EnableCover();
            secondSelectedItem.EnableCover();
        }

        firstSelectedItem = null;
        secondSelectedItem = null;

        if (canvasGroup != null) canvasGroup.interactable = true;

        if (numberOfMatches == listOfCards.Count / 2)
        {
            StartCoroutine(LoadFinalScene());
        }
    }

    IEnumerator LoadFinalScene()
    {
        GameManager.SetSeconds(timerScript.GetTimerAndStop());
        if (victoryMusic != null) victoryMusic.Play();
        yield return new WaitForSeconds(victoryMusic != null ? victoryMusic.clip.length : 2f);
        SceneManager.LoadScene("FinalScene");
    }
}