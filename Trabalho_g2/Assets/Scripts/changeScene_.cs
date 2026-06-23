using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Esta função vai receber o nome da cena do botão
    public void MudarCena(string nomeDaCena)
    {
        if (!string.IsNullOrEmpty(nomeDaCena))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nomeDaCena);
        }
        else
        {
            Debug.LogWarning("Escreve o nome da cena no botão!");
        }
    }
}