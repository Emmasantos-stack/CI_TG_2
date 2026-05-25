using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour
{
    public string sceneName;

    public void GoToWorld()
    {
        SceneManager.LoadScene(sceneName);
    }
}