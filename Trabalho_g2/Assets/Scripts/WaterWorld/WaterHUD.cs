using TMPro;
using UnityEngine;

public class WaterHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text resultMessageText;
    [SerializeField] private TMP_Text finalScoreText;

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public void SetLives(int lives)
    {
        if (livesText != null)
        {
            livesText.text = $"Vidas: {lives}";
        }
    }

    public void SetInstructions(bool visible)
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(visible);
        }
    }

    public void SetTime(float remainingTime)
    {
        if (timeText != null)
        {
            timeText.text = $"Tempo: {Mathf.CeilToInt(remainingTime)}";
        }
    }

    public void SetGameOver(bool visible, int finalScore, bool victory)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(visible);
        }

        if (visible && resultMessageText != null)
        {
            resultMessageText.text = victory ? "Vitória!" : "Fim de Jogo!";
        }

        if (visible && finalScoreText != null)
        {
            finalScoreText.text = $"Score final: {finalScore}";
        }
    }
}
