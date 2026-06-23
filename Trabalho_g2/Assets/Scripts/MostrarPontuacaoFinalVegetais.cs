using UnityEngine;
using TMPro;

public class MostrarPontuacaoFinal : MonoBehaviour
{
    [Header("Componentes de Texto (UI)")]
    public TextMeshProUGUI textoPontos;    // Arrasta o objeto "Pontuacao"
    public TextMeshProUGUI textoMensagem;  // Arrasta o objeto "Mensagem"
    public TextMeshProUGUI textoTitulo;    // Arrasta o objeto "ResultadoFinal" se quiseres mudar o título

    [Header("Objetos das Medalhas da Hierarchy")]
    // Aqui arrastas os objetos normais que já tens na tua lista esquerda
    public GameObject medalhaOuro;
    public GameObject medalhaPrata;
    public GameObject medalhaBronze;

    [Header("Definição de Pontos")]
    public int pontosParaOuro = 100;
    public int pontosParaPrata = 50;

    void Start()
    {
        // Vai buscar os pontos guardados na memória
        int pontuacaoFinalTotal = PlayerPrefs.GetInt("PontuacaoTotal", 0);

        // 1. Atualiza o número de pontos no ecrã
        if (textoPontos != null)
        {
            textoPontos.text = pontuacaoFinalTotal.ToString() + " Pontos";
        }

        // 2. Controla as medalhas e as frases com base nos pontos
        AtualizarPremios(pontuacaoFinalTotal);
    }

    void AtualizarPremios(int pontos)
    {
        // Primeiro desliga todas as medalhas da cena para não se atropelarem
        if (medalhaOuro != null) medalhaOuro.SetActive(false);
        if (medalhaPrata != null) medalhaPrata.SetActive(false);
        if (medalhaBronze != null) medalhaBronze.SetActive(false);

        // Verifica os patamares
        if (pontos >= pontosParaOuro)
        {
            // Ativa a medalha de ouro na Hierarchy
            if (medalhaOuro != null) medalhaOuro.SetActive(true);
            
            // Muda a frase de cima
            if (textoMensagem != null) textoMensagem.text = "Excelente trabalho! És um mestre!";
            if (textoTitulo != null) textoTitulo.text = "VITÓRIA BRUTAL!";
        }
        else if (pontos >= pontosParaPrata)
        {
            // Ativa a medalha de prata na Hierarchy
            if (medalhaPrata != null) medalhaPrata.SetActive(true);
            
            // Muda a frase de cima
            if (textoMensagem != null) textoMensagem.text = "Muito bom! Quase perfeito!";
            if (textoTitulo != null) textoTitulo.text = "BOM TRABALHO!";
        }
        else
        {
            // Ativa a medalha de bronze na Hierarchy
            if (medalhaBronze != null) medalhaBronze.SetActive(true);
            
            // Muda a frase de cima
            if (textoMensagem != null) textoMensagem.text = "Concluíste! Tenta outra vez para melhorar!";
            if (textoTitulo != null) textoTitulo.text = "FIM DO JOGO";
        }
    }
}