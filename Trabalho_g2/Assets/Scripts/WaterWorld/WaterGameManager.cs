using UnityEngine;

public class WaterGameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField, Min(1)] private int startingLives = 3;

    [Header("References")]
    [SerializeField] private WaterHUD hud;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioClip damageClip;

    public int Score { get; private set; }
    public int Lives { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Score = 0;
        Lives = startingLives;
        IsGameOver = false;
    }

    private void Start()
    {
        if (hud != null)
        {
            hud.SetScore(Score);
            hud.SetLives(Lives);
            hud.SetGameOver(false, Score);
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

    public void AddScore(int amount)
    {
        if (IsGameOver || amount <= 0)
        {
            return;
        }

        Score += amount;
        hud?.SetScore(Score);
        PlaySfx(collectClip);
    }

    public void LoseLife(int amount)
    {
        if (IsGameOver || amount <= 0)
        {
            return;
        }

        Lives = Mathf.Max(0, Lives - amount);
        hud?.SetLives(Lives);
        PlaySfx(damageClip);

        if (Lives == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        IsGameOver = true;
        hud?.SetGameOver(true, Score);
    }

    private void PlaySfx(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
