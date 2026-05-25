using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public int answerIndex;
    public QuizManager quizManager;

    public void OnClick()
    {
        quizManager.Answer(answerIndex);
    }
}