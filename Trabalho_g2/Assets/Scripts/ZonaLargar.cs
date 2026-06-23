using UnityEngine;
using UnityEngine.EventSystems;

public class ZonaLargar : MonoBehaviour, IDropHandler
{
    [Header("Configuração da Zona")]
    public bool aceitaSaudavel; 

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject alimento = eventData.pointerDrag;
            bool ehSaudavel = VerificarSeEhSaudavel(alimento.name);

            if (ehSaudavel == aceitaSaudavel)
            {
                alimento.transform.SetParent(this.transform);
                
                // --- SISTEMA DE GRAVAÇÃO DIRETA ---
                // 1. Vai buscar o grande total acumulado que já existia na memória
                int totalAtual = PlayerPrefs.GetInt("PontuacaoTotal", 0);

                // 2. Soma 10 pontos por este acerto
                totalAtual += 10;

                // 3. Grava imediatamente o novo valor total na memória
                PlayerPrefs.SetInt("PontuacaoTotal", totalAtual);
                PlayerPrefs.Save();

                Debug.Log("Acertou! Novo Total Geral na Memória: " + totalAtual);
            }
            else
            {
                // Se errou, podemos tirar 5 pontos (opcional)
                int totalAtual = PlayerPrefs.GetInt("PontuacaoTotal", 0);
                totalAtual -= 5;
                if (totalAtual < 0) totalAtual = 0;

                PlayerPrefs.SetInt("PontuacaoTotal", totalAtual);
                PlayerPrefs.Save();

                Debug.Log("Errou! Novo Total Geral na Memória: " + totalAtual);
            }
        }
    }

    private bool VerificarSeEhSaudavel(string nomeAlimento)
    {
        nomeAlimento = nomeAlimento.ToLower();
        
        // Mantém a tua lista de alimentos aqui
        if (nomeAlimento.Contains("brocolo") || 
            nomeAlimento.Contains("cenoura") || 
            nomeAlimento.Contains("alface") || 
            nomeAlimento.Contains("tomate") ||
            nomeAlimento.Contains("couve") ||
            nomeAlimento.Contains("maca") ||
            nomeAlimento.Contains("couveflor")) 
        {
            return true;
        }
        
        return false; 
    }
}