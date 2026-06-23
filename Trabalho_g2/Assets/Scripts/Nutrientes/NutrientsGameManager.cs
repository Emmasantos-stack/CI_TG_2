using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NutrientsGameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public Button nextLevelButton;

    public int pontosNecessarios = 3;
    public string proximaCena;

    public bool usarTimer = false;
    public float tempo = 30f;

    private int pontos = 0;
    private bool jogoAtivo = true;

    void Start()
    {
        if (timerText != null)
            timerText.gameObject.SetActive(usarTimer);

        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(false);

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
            Invoke(nameof(ReiniciarNivel), 1.5f);
        }

        AtualizarUI();
    }

    public void AdicionarPonto()
    {
        if (!jogoAtivo) return;

        pontos++;

        if (pontos >= pontosNecessarios)
            Ganhou();

        AtualizarUI();
    }

    public void RemoverPonto()
    {
        if (!jogoAtivo) return;

        pontos = Mathf.Max(0, pontos - 1);
        AtualizarUI();
    }

    void Ganhou()
    {
        jogoAtivo = false;

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