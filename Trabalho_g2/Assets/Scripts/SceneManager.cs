using UnityEngine;
using UnityEngine.SceneManagement; // Obrigatório para controlar as cenas

public class GerenciadorCenas : MonoBehaviour
{
    // Criar um Singleton para podermos chamar este script de qualquer lado
    public static GerenciadorCenas Instancia { get; private set; }

    void Awake()
    {
        // Garante que só existe UM gerenciador de cenas no jogo todo
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); // Não destrói este objeto ao mudar de cena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Função para carregar uma cena pelo NOME
    public void CarregarCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    // Função para reiniciar a cena atual (útil para botões de "Tentar Novamente")
    public void ReiniciarCenaAtual()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(cenaAtual);
    }

    // Função para fechar o jogo (só funciona no jogo compilado/Build, não no Editor)
    public void SairDoJogo()
    {
        Debug.Log("O jogo fechou!");
        Application.Quit();
    }
}