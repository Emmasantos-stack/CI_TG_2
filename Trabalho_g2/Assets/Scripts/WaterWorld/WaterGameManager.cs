using UnityEngine;
using UnityEngine.SceneManagement;


public class WaterGameManager : MonoBehaviour
{
    private const int MediumScoreThreshold = 50;
    private const int HardScoreThreshold = 100;

    [Header("Game")]
    [SerializeField, Min(1)] private int startingLives = 3;
    [SerializeField, Min(1f)] private float gameDuration = 60f;
    [SerializeField] private string menuSceneName = "SeleçaoMundo";


    [Header("References")]
    [SerializeField] private WaterHUD hud;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioClip damageClip;

    public int Score { get; private set; }
    public int Lives { get; private set; }
    public float RemainingTime { get; private set; }
    public FallingItemSpawner.Difficulty SelectedDifficulty { get; private set; }
    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Score = 0;
        Lives = startingLives;
        RemainingTime = gameDuration;
        SelectedDifficulty = FallingItemSpawner.Difficulty.Easy;
        IsGameStarted = false;
        IsGameOver = false;
    }

    private void Start()
    {
        if (hud != null)
        {
            hud.SetScore(Score);
            hud.SetLives(Lives);
            hud.SetTime(RemainingTime);
            hud.SetInstructions(true);
            hud.SetGameOver(false, Score, false);
        }

        if (musicSource != null && musicSource.clip != null)
        {
            musicSource.loop = true;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    private void Update()
    {
        if (!IsGameStarted || IsGameOver)
        {
            return;
        }

        RemainingTime = Mathf.Max(0f, RemainingTime - Time.deltaTime);
        hud?.SetTime(RemainingTime);

        if (RemainingTime <= 0f && Lives > 0)
        {
            EndGame(true);
        }
    }

    public void StartGame()
    {
        if (IsGameStarted || IsGameOver)
        {
            return;
        }

        RemainingTime = gameDuration;
        IsGameStarted = true;
        hud?.SetTime(RemainingTime);
        hud?.SetInstructions(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        if (string.IsNullOrWhiteSpace(menuSceneName))
        {
            Debug.LogWarning("Menu Scene Name não está configurado.", this);
            return;
        }

        SceneManager.LoadScene(menuSceneName);
    }

    public void AddScore(int amount)
    {
        if (!IsGameStarted || IsGameOver || amount <= 0)
        {
            return;
        }

        Score += amount;
        UpdateDifficulty();
        hud?.SetScore(Score);
        PlaySfx(collectClip);
    }

    public void LoseLife(int amount)
    {
        if (!IsGameStarted || IsGameOver || amount <= 0)
        {
            return;
        }

        Lives = Mathf.Max(0, Lives - amount);
        hud?.SetLives(Lives);
        PlaySfx(damageClip);

        if (Lives == 0)
        {
            EndGame(false);
        }
    }

    private void EndGame(bool victory)
    {
        IsGameOver = true;
        hud?.SetGameOver(true, Score, victory);
    }

    private void UpdateDifficulty()
    {
        FallingItemSpawner.Difficulty newDifficulty = FallingItemSpawner.Difficulty.Easy;

        if (Score >= HardScoreThreshold)
        {
            newDifficulty = FallingItemSpawner.Difficulty.Hard;
        }
        else if (Score >= MediumScoreThreshold)
        {
            newDifficulty = FallingItemSpawner.Difficulty.Medium;
        }

        if (newDifficulty == SelectedDifficulty)
        {
            return;
        }

        SelectedDifficulty = newDifficulty;
        Debug.Log($"Dificuldade alterada para {SelectedDifficulty}");
    }

    private void PlaySfx(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
