using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class Temporizador : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float tempoRestante = 60f; 
    public bool temporizadorAtivo = true;

    [Header("Componentes de UI")]
    public TextMeshProUGUI textoTempo; 

    [Header("Mudança de Cena")]
    [Tooltip("Escreve aqui no Inspector o nome EXATO da tua cena final.")]
    public string nomeCenaPontuacoes = "Pontuacoes"; 

    void Update()
    {
        if (temporizadorAtivo)
        {
            if (tempoRestante > 0)
            {
                tempoRestante -= Time.deltaTime;
                AtualizarTextoTempo(tempoRestante);
            }
            else
            {
                tempoRestante = 0;
                temporizadorAtivo = false;
                TempoAcabou();
            }
        }
    }

    void AtualizarTextoTempo(float tempoParaMostrar)
    {
        if (tempoParaMostrar < 0) tempoParaMostrar = 0;
        int minutos = Mathf.FloorToInt(tempoParaMostrar / 60);
        int segundos = Mathf.FloorToInt(tempoParaMostrar % 60);
        textoTempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void TempoAcabou()
    {
        Debug.Log("O tempo terminou! A carregar a cena: " + nomeCenaPontuacoes);
        
        // Carrega a cena que foi definida na caixinha do Inspector
        SceneManager.LoadScene(nomeCenaPontuacoes);
    }
}