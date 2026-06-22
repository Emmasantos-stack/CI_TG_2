using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para mudar de cena

public class ChangeScene : MonoBehaviour
{
    // Esta função pode ser chamada por qualquer botão no Unity
    public void MudarCena(string nomeDaCena)
    {
        // Verifica se não escreveste o nome vazio por engano
        if (!string.IsNullOrEmpty(nomeDaCena))
        {
            SceneManager.LoadScene(nomeDaCena);
        }
        else
        {
            Debug.LogWarning("Por favor, digita o nome da cena no componente do Botão!");
        }
    }
}