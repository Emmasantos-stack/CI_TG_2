using UnityEngine;
using UnityEngine.EventSystems;

public class ZonaLargar : MonoBehaviour, IDropHandler
{
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
                Debug.Log("Acertou!");
            }
            else
            {
                Debug.Log("Alimento no local errado!");
            }
        }
    }

    private bool VerificarSeEhSaudavel(string nomeAlimento)
    {
        nomeAlimento = nomeAlimento.ToLower();
        
        if (nomeAlimento.Contains("brocolo") || nomeAlimento.Contains("cenoura") || nomeAlimento.Contains("alface") || nomeAlimento.Contains("tomate"))
        {
            return true;
        }
        
        return false; 
    }
}