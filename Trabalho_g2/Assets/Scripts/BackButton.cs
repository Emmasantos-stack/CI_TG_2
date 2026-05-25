using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public string menuScene = "MenuPrincipal";

    public void GoBack()
    {
        SceneManager.LoadScene(menuScene);
    }
}