using TMPro;
using UnityEngine;

public class WaterHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject gameOverPanel;
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

    public void SetGameOver(bool visible, int finalScore)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(visible);
        }

        if (visible && finalScoreText != null)
        {
            finalScoreText.text = $"Score final: {finalScore}";
        }
    }
}
