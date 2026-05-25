using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public Image fadeImage;
    public float speed = 2f;

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(Fade(sceneName));
    }

    IEnumerator Fade(string sceneName)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * speed;
            fadeImage.color = new Color(0,0,0,t);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}