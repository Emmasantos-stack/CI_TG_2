using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControleExplicacao : MonoBehaviour
{
    [Header("Componentes de UI")]
    public TextMeshProUGUI campoTextoBalao; 
    public GameObject botaoJogar; 

    [Header("Configuração dos Grupos de Frutas")]
    public GameObject[] gruposFrutas; 

    [Header("Textos da Explicação")]
    [TextArea(3, 5)] 
    public string[] textosExplicacao; 

    [Header("Tempos de Exibição (em segundos)")]
    public float[] temposDeEspera; 

    void Start()
    {
        ConfigurarInicio();
        StartCoroutine(SequenciaExplicacao());
    }

    void ConfigurarInicio()
    {
        if (botaoJogar != null)
        {
            botaoJogar.SetActive(false);
        }

        foreach (GameObject fruta in gruposFrutas)
        {
            if (fruta != null) 
            {
                fruta.SetActive(false);
            }
        }
        
        if (campoTextoBalao != null)
        {
            campoTextoBalao.text = "";
        }
    }

    IEnumerator SequenciaExplicacao()
    {
        for (int i = 0; i < gruposFrutas.Length; i++)
        {
            if (gruposFrutas[i] != null)
            {
                gruposFrutas[i].SetActive(true);
            }

            if (campoTextoBalao != null && textosExplicacao.Length > i)
            {
                campoTextoBalao.text = textosExplicacao[i];
            }

            if (temposDeEspera.Length > i)
            {
                yield return new WaitForSeconds(temposDeEspera[i]);
            }
            else
            {
                yield return new WaitForSeconds(5f); 
            }

            if (gruposFrutas[i] != null)
            {
                gruposFrutas[i].SetActive(false);
            }
        }

        if (campoTextoBalao != null)
        {
            campoTextoBalao.text = "Agora que já conheces as frutas, que tal um desafio?";
        }
        
        if (botaoJogar != null)
        {
            botaoJogar.SetActive(true); 
        }

        Debug.Log("Explicação terminada. Botão de jogar ativo!");
    }

        public void AvancarParaMiniJogo()
    {
        // Substitui "NomeDoTeuMiniJogo" pelo nome exato da tua nova cena
        SceneManager.LoadScene("fruta_3");
    }

}