using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultadoFinal : MonoBehaviour
{
    public TextMeshProUGUI pontuacaoText;
    public TextMeshProUGUI mensagemText;

    public Image medalhaImage;

    public Sprite bronze;
    public Sprite prata;
    public Sprite ouro;

    void Start()
    {
        int pontos = PlayerPrefs.GetInt("PontuacaoFinal", 0);

        pontuacaoText.text = pontos + " pontos";

        if (pontos >= 200)
        {
            medalhaImage.sprite = ouro;
            mensagemText.text = "Excelente trabalho!";
        }
        else if (pontos >= 120)
        {
            medalhaImage.sprite = prata;
            mensagemText.text = "Muito bem!";
        }
        else
        {
            medalhaImage.sprite = bronze;
            mensagemText.text = "Continua a tentar!";
        }
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene("Quiz");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void IrCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}