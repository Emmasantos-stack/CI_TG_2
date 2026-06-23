using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using Random = UnityEngine.Random;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    private List<CardScript> listOfCards; 

    [Header("Imagens do Jogo de Memória")]
    [SerializeField]
    private List<Sprite> spritesFrutas;    

    [SerializeField]
    private List<Sprite> spritesVitaminas; 

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
        if (spritesFrutas == null || spritesVitaminas == null || listOfCards == null) return;
        if (spritesFrutas.Count != spritesVitaminas.Count || listOfCards.Count != spritesFrutas.Count * 2) return;

        // Distribuir os pares pelas cartas com segurança
        int cardIndex = 0;
        for (int i = 0; i < spritesFrutas.Count; i++)
        {
            if (cardIndex < listOfCards.Count && listOfCards[cardIndex] != null)
                listOfCards[cardIndex].SetCardData(spritesFrutas[i], i);
            cardIndex++;

            if (cardIndex < listOfCards.Count && listOfCards[cardIndex] != null)
                listOfCards[cardIndex].SetCardData(spritesVitaminas[i], i);
            cardIndex++;
        }

        // Executa o Shuffle com uma pequena folga de tempo para a UI
        StartCoroutine(ExecutarShuffleGarantido());
    }

    IEnumerator ExecutarShuffleGarantido()
    {
        yield return new WaitForEndOfFrame(); 
        Shuffle(listOfCards);
    }

    void Shuffle(List<CardScript> list)
    {
        if (list == null || list.Count <= 1) return;

        // 1. Desliga temporariamente os Layout Groups automáticos se existirem no Pai com segurança
        Transform objetoPai = null;
        foreach (var card in list)
        {
            if (card != null)
            {
                objetoPai = card.transform.parent;
                break;
            }
        }

        LayoutGroup layoutGroup = null;
        if (objetoPai != null)
        {
            layoutGroup = objetoPai.GetComponent<LayoutGroup>();
            if (layoutGroup != null) layoutGroup.enabled = false;
        }

        // 2. Guarda apenas as posições de UI válidas (evita o erro da linha 70)
        List<Vector2> posicoesUI = new List<Vector2>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                RectTransform rect = list[i].GetComponent<RectTransform>();
                if (rect != null)
                {
                    posicoesUI.Add(rect.anchoredPosition);
                }
            }
        }

        // 3. Baralha a lista de posições guardadas (Algoritmo Fisher-Yates)
        int n = posicoesUI.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Vector2 temp = posicoesUI[k];
            posicoesUI[k] = posicoesUI[n];
            posicoesUI[n] = temp;
        }

        // 4. Aplica as novas posições baralhadas de volta às cartas reais
        int posicaoIndex = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null && posicaoIndex < posicoesUI.Count)
            {
                RectTransform rect = list[i].GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.anchoredPosition = posicoesUI[posicaoIndex];
                    posicaoIndex++;
                }
            }
        }

        // 5. Reativa o layout se existia para recalcular a interface
        if (layoutGroup != null && objetoPai != null)
        {
            layoutGroup.enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(objetoPai.GetComponent<RectTransform>());
        }

        Debug.Log("Shuffle concluído sem erros!");
    }

    public void OnCardClick()
    {
        if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null) return;
        if (firstSelectedItem && secondSelectedItem) return;

        var clickedItem = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CardScript>();
        if (clickedItem == null || clickedItem == firstSelectedItem) return;

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

    private void CompareChosenItems()
    {
        if (firstSelectedItem == null || secondSelectedItem == null) return;

        if (firstSelectedItem.cardID == secondSelectedItem.cardID)
        {
            numberOfMatches++;
            StartCoroutine(ResetAndCheckFinish(0, false));
        }
        else
        {
            StartCoroutine(ResetAndCheckFinish(1.5f, true));
        }
    }

    IEnumerator ResetAndCheckFinish(float numberOfSecondsToWait, bool shouldReset)
    {
        if (canvasGroup != null) canvasGroup.interactable = false;
        yield return new WaitForSeconds(numberOfSecondsToWait);

        if (shouldReset)
        {
            if (firstSelectedItem != null) firstSelectedItem.EnableCover();
            if (secondSelectedItem != null) secondSelectedItem.EnableCover();
        }

        firstSelectedItem = null;
        secondSelectedItem = null;
        if (canvasGroup != null) canvasGroup.interactable = true;

        if (numberOfMatches == listOfCards.Count / 2) StartCoroutine(LoadFinalScene());
    }

    IEnumerator LoadFinalScene()
    {
        if (timerScript != null) GameManager.SetSeconds(timerScript.GetTimerAndStop());
        if (victoryMusic != null) victoryMusic.Play();
        yield return new WaitForSeconds(victoryMusic != null ? victoryMusic.clip.length : 2f);
        SceneManager.LoadScene("FinalScene");
    }
}