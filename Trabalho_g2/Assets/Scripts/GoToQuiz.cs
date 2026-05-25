using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToQuiz : MonoBehaviour
{
    public string quizScene = "Quiz";

    public void StartQuiz()
    {
        SceneManager.LoadScene(quizScene);
    }
}