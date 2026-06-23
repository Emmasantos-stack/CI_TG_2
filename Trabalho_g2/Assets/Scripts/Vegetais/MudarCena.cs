using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANTE: Dá acesso ao gestor de cenas

public class MudarCena : MonoBehaviour
{
    // Função que vai ser chamada quando clicares no botão
    public void CarregarProximoNivel(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}