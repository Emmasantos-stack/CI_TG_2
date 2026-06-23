using UnityEngine;
using TMPro; // Necessário para usar o TextMeshPro
using UnityEngine.SceneManagement; // Necessário para mudar de cena

public class Temporizador : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float tempoRestante = 60f; // Tempo inicial em segundos
    public bool temporizadorAtivo = true;

    [Header("Componentes de UI")]
    public TextMeshProUGUI textoTempo; // Arrasta o teu texto da UI para aqui

    [Header("Mudança de Cena")]
    [Tooltip("Escreve aqui o nome exato da cena de pontuações.")]
    public string nomeCenaPontuacoes = "Pontuacoes"; 

    void Update()
    {
        if (temporizadorAtivo)
        {
            if (tempoRestante > 0)
            {
                // Subtrai o tempo passado desde o último frame
                tempoRestante -= Time.deltaTime;
                
                // Atualiza o texto no ecrã
                AtualizarTextoTempo(tempoRestante);
            }
            else
            {
                // O TEMPO ACABOU!
                tempoRestante = 0;
                temporizadorAtivo = false;
                TempoAcabou();
            }
        }
    }

    void AtualizarTextoTempo(float tempoParaMostrar)
    {
        if (tempoParaMostrar < 0) tempoParaMostrar = 0;

        // Calcula minutos e segundos
        int minutos = Mathf.FloorToInt(tempoParaMostrar / 60);
        int segundos = Mathf.FloorToInt(tempoParaMostrar % 60);

        // Formata para o formato padrão de relógio (ex: 00:45)
        textoTempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void TempoAcabou()
    {
        Debug.Log("O tempo terminou! A carregar a cena de pontuações...");
        
        // Carrega a cena de pontuações que definiste no Inspector
        SceneManager.LoadScene(nomeCenaPontuacoes);
    }
}