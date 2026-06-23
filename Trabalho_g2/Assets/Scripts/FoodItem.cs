using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NutrientsGameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public float tempo = 30f;
    public int pontosNecessarios = 3;

    public string proximaCena;

    private int pontos = 0;
    private bool jogoAtivo = true;

    void Start()
    {
        AtualizarUI();
    }

    void Update()
    {
        if (!jogoAtivo) return;

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
            Ganhou();
        }

        AtualizarUI();
    }

    public void RemoverPonto()
    {
        if (!jogoAtivo) return;

        pontos--;

        if (pontos < 0)
            pontos = 0;

        AtualizarUI();
    }

    void AtualizarUI()
    {
        timerText.text = "Tempo: " + Mathf.CeilToInt(tempo);
        scoreText.text = "Pontos: " + pontos + " / " + pontosNecessarios;
    }

    void Ganhou()
    {
        SceneManager.LoadScene(proximaCena);
    }

    void Perdeu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}