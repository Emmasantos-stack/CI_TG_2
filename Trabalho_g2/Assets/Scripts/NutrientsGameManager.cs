using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NutrientsGameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;
    public Button nextLevelButton;

    public bool usarTimer = false;
    public float tempo = 30f;
    public int pontosNecessarios = 3;
    public string proximaCena;

    private int pontos = 0;
    private bool jogoAtivo = true;

    void Start()
    {
        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(false);

        if (!usarTimer && timerText != null)
            timerText.gameObject.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";

        AtualizarUI();
    }

    void Update()
    {
        if (!jogoAtivo || !usarTimer) return;

        tempo -= Time.deltaTime;

        if (tempo <= 0)
        {
            tempo = 0;
            jogoAtivo = false;
            Perdeu();
        }

        AtualizarUI();
    }

    public void AdicionarPonto()
    {
        if (!jogoAtivo) return;

        pontos++;
        MostrarFeedback("Boa escolha!", Color.green);

        if (pontos >= pontosNecessarios)
            Ganhou();

        AtualizarUI();
    }

    public void RemoverPonto()
    {
        if (!jogoAtivo) return;

        pontos = Mathf.Max(0, pontos - 1);
        MostrarFeedback("Esse não é tão saudável!", Color.red);

        AtualizarUI();
    }

    void Ganhou()
    {
        jogoAtivo = false;
        MostrarFeedback("Prato completo!", Color.green);

        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(true);
        else
            SceneManager.LoadScene(proximaCena);
    }

    void Perdeu()
    {
        MostrarFeedback("O tempo acabou!", Color.red);
        Invoke(nameof(ReiniciarNivel), 2f);
    }

    public void IrParaProximoNivel()
    {
        SceneManager.LoadScene(proximaCena);
    }

    void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void AtualizarUI()
    {
        if (usarTimer && timerText != null)
            timerText.text = "Tempo: " + Mathf.CeilToInt(tempo);

        if (scoreText != null)
            scoreText.text = "Pontos: " + pontos + " / " + pontosNecessarios;
    }

    void MostrarFeedback(string mensagem, Color cor)
    {
        if (feedbackText == null) return;

        feedbackText.text = mensagem;
        feedbackText.color = cor;
    }
}