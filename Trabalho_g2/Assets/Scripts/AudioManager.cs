using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musica;

    public Sprite somLigado;
    public Sprite somDesligado;

    public Image botaoSom;

    private bool ligado = true;

    public void ToggleSom()
    {
        ligado = !ligado;

        if (ligado)
        {
            musica.volume = 1f;
            botaoSom.sprite = somLigado;
        }
        else
        {
            musica.volume = 0f;
            botaoSom.sprite = somDesligado;
        }
    }
}