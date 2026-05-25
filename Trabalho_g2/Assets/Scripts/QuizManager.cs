using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswer;
        public Sprite background;
    }

    public Question[] questions;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerTexts;
    public Image backgroundImage;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        Question q = questions[currentQuestion];

        questionText.text = q.questionText;

        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = q.answers[i];
        }

        StartCoroutine(ChangeBackground(q.background));
    }

    public void Answer(int index)
    {
        if (index == questions[currentQuestion].correctAnswer)
        {
            score++;
            Debug.Log("Certo!");
        }
        else
        {
            Debug.Log("Errado!");
        }

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            ShowResult();
        }
    }

    public MedalSystem medalSystem;
    void ShowResult()
    {
        resultPanel.SetActive(true);
        resultText.text = "Pontuação: " + score + "/" + questions.Length;

        medalSystem.SetMedal(score, questions.Length);
    }
    IEnumerator ChangeBackground(Sprite newBg)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2;
            backgroundImage.color = new Color(1,1,1,1 - t);
            yield return null;
        }

        backgroundImage.sprite = newBg;

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2;
            backgroundImage.color = new Color(1,1,1,t);
            yield return null;
        }
    }
}