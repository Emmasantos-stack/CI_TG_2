using UnityEngine;
using UnityEngine.SceneManagement;

public class EcraInicialManager : MonoBehaviour
{
    public void IrParaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}