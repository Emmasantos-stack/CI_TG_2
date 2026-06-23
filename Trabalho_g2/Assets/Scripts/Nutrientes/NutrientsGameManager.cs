using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NutrientsGameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI feedbackText;

    public Button nextLevelButton;

    public int pontosNecessarios = 3;
    public string proximaCena;

    public bool usarTimer = false;
    public float tempo = 30f;

    private int pontos = 0;
    private bool jogoAtivo = true;

    void Start()
    {
        AtualizarUI();

        if (timerText != null)
            timerText.gameObject.SetActive(usarTimer);

        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";
    }

    void Update()
    {
        if (!jogoAtivo || !usarTimer) return;

        tempo -= Time.deltaTime;

        if (tempo <= 0)
        {
            tempo = 0;
            jogoAtivo = false;
            feedbackText.text = "Tempo esgotado!";
            Invoke(nameof(ReiniciarNivel), 2f);
        }

        AtualizarUI();
    }

    public void AdicionarPonto()
    {
        if (!jogoAtivo) return;

        pontos++;

        if (feedbackText != null)
        {
            feedbackText.text = "Boa escolha!";
            feedbackText.color = Color.green;
        }

        if (pontos >= pontosNecessarios)
            Ganhou();

        AtualizarUI();
    }

    public void RemoverPonto()
    {
        if (!jogoAtivo) return;

        pontos = Mathf.Max(0, pontos - 1);

        if (feedbackText != null)
        {
            feedbackText.text = "Esse alimento não é tão saudável!";
            feedbackText.color = Color.red;
        }

        AtualizarUI();
    }

    void Ganhou()
    {
        jogoAtivo = false;

        if (feedbackText != null)
        {
            feedbackText.text = "Prato saudável completo!";
            feedbackText.color = Color.green;
        }

        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(true);
        else
            SceneManager.LoadScene(proximaCena);
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
        if (scoreText != null)
            scoreText.text = pontos + "/" + pontosNecessarios;

        if (timerText != null && usarTimer)
            timerText.text = "Tempo: " + Mathf.CeilToInt(tempo);
    }
}