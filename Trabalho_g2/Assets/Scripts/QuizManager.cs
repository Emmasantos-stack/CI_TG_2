using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswer;
    public Sprite character;
}

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerTexts;
    public Button[] answerButtons;

    public Image characterImage;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI progressText;

   public Slider progressBar;
    public Question[] questions;

    private int currentQuestion = 0;
    private int score = 0;
    private bool canAnswer = true;

    void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        canAnswer = true;
        feedbackText.text = "";

        Question q = questions[currentQuestion];

        questionText.text = q.questionText;
        characterImage.sprite = q.character;

        progressBar.maxValue = questions.Length;
        progressBar.value = currentQuestion + 1;

        progressText.text = "Pergunta " + (currentQuestion + 1) + " / " + questions.Length;
        

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;

            answerTexts[i].text = q.answers[i];

            answerButtons[i].interactable = true;
            answerButtons[i].GetComponent<Image>().color = Color.white;

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    public void CheckAnswer(int index)
    {
        if (!canAnswer) return;

        canAnswer = false;

        Question q = questions[currentQuestion];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;
        }

        if (index == q.correctAnswer)
        {
            score += 10;
            answerButtons[index].GetComponent<Image>().color = Color.green;
            feedbackText.text = "Boa! Resposta certa!";
        }
        else
        {
            answerButtons[index].GetComponent<Image>().color = Color.red;
            answerButtons[q.correctAnswer].GetComponent<Image>().color = Color.green;
            feedbackText.text = "Ops! A resposta certa era: " + q.answers[q.correctAnswer];
        }

        Invoke(nameof(NextQuestion), 2f);
    }

    void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            PlayerPrefs.SetInt("PontuacaoFinal", score);
            SceneManager.LoadScene("ResultadoFinal");
        }
    }
}