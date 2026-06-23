using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NutrientsGameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Jogo")]
    public bool usarTimer = false;

    public float tempo = 30f;

    public int pontosNecessarios = 3;

    public string proximaCena;

    private int pontos = 0;

    private bool jogoAtivo = true;

    void Start()
    {
        if (!usarTimer && timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        AtualizarUI();
    }

    void Update()
    {
        if (!jogoAtivo)
            return;

        if (!usarTimer)
            return;

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
        if (!jogoAtivo)
            return;

        pontos++;

        AtualizarUI();

        if (pontos >= pontosNecessarios)
        {
            jogoAtivo = false;

            SceneManager.LoadScene(proximaCena);
        }
    }

    public void RemoverPonto()
    {
        if (!jogoAtivo)
            return;

        pontos--;

        if (pontos < 0)
            pontos = 0;

        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                pontos + " / " + pontosNecessarios;
        }

        if (usarTimer && timerText != null)
        {
            timerText.text =
                Mathf.CeilToInt(tempo).ToString();
        }
    }

    void Perdeu()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}