using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musica;
    public Image botaoImagem;
    public Sprite somLigado;
    public Sprite somDesligado;

    private bool ligado = true;

    public void ToggleSom()
    {
        ligado = !ligado;

        musica.mute = !ligado;

        if (ligado)
            botaoImagem.sprite = somLigado;
        else
            botaoImagem.sprite = somDesligado;
    }
}