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
            
            // Verifica o nome do ficheiro/objeto na Hierarchy
            bool ehSaudavel = VerificarSeEhSaudavel(alimento.name);

            // Se o tipo de alimento bater certo com a zona onde o largaste
            if (ehSaudavel == aceitaSaudavel)
            {
                alimento.transform.SetParent(this.transform);
                Debug.Log("Acertou: " + alimento.name);
            }
            else
            {
                Debug.Log("Errou: " + alimento.name + " não entra nesta caixa.");
            }
        }
    }

    private bool VerificarSeEhSaudavel(string nomeAlimento)
    {
        nomeAlimento = nomeAlimento.ToLower();
        
        // Lista de alimentos saudáveis (adiciona aqui os novos do nível 2 se quiseres)
        if (nomeAlimento.Contains("brocolo") || 
            nomeAlimento.Contains("cenoura") || 
            nomeAlimento.Contains("alface") || 
            nomeAlimento.Contains("tomate") ||
            nomeAlimento.Contains("couve")) 
        {
            return true;
        }
        
        // Se não for nenhum dos de cima (Gelado, Chocolate, Hambúrguer, Cachorro), é Falso (Não Saudável)
        return false; 
    }
}