using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NutrientsGameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public bool usarTimer = false;
    public float tempo = 30f;

    public int pontosNecessarios = 3;
    public string proximaCena;

    private int pontos = 0;
    private bool jogoAtivo = true;

    void Start()
    {
        if (!usarTimer && timerText != null)
            timerText.gameObject.SetActive(false);

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

        if (pontos >= pontosNecessarios)
        {
            jogoAtivo = false;
            SceneManager.LoadScene(proximaCena);
        }

        AtualizarUI();
    }

    public void RemoverPonto()
    {
        if (!jogoAtivo) return;

        pontos = Mathf.Max(0, pontos - 1);
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (usarTimer && timerText != null)
            timerText.text = "Tempo: " + Mathf.CeilToInt(tempo);

        if (scoreText != null)
            scoreText.text = "Pontos: " + pontos + " / " + pontosNecessarios;
    }

    void Perdeu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}