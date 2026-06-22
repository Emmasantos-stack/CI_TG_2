using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text answersText;

    void Start()
    {
        answersText.text = GameManager.GetSeconds().ToString();
    }

    public void TestAgain()
    {
        GameManager.Reset();

        SceneManager.LoadScene("Main");
    }
}