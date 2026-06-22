using System.Collections;
using UnityEngine;

public class FallingItemSpawner : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    [System.Serializable]
    private class DifficultySettings
    {
        [Min(0.1f)] public float spawnInterval = 1f;
        [Range(0f, 1f)] public float obstacleChance = 0.3f;
        [Min(0.1f)] public float minimumFallSpeed = 3.5f;
        [Min(0.1f)] public float maximumFallSpeed = 5f;
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject waterDropPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    [Header("References")]
    [SerializeField] private Camera gameplayCamera;
    [SerializeField] private WaterGameManager gameManager;

    [Header("Difficulty")]
    [SerializeField] private Difficulty difficulty = Difficulty.Easy;
    [SerializeField] private DifficultySettings easy = new DifficultySettings
    {
        spawnInterval = 1.2f,
        obstacleChance = 0.2f,
        minimumFallSpeed = 3f,
        maximumFallSpeed = 4f
    };
    [SerializeField] private DifficultySettings medium = new DifficultySettings
    {
        spawnInterval = 0.9f,
        obstacleChance = 0.3f,
        minimumFallSpeed = 4f,
        maximumFallSpeed = 5.5f
    };
    [SerializeField] private DifficultySettings hard = new DifficultySettings
    {
        spawnInterval = 0.65f,
        obstacleChance = 0.4f,
        minimumFallSpeed = 5f,
        maximumFallSpeed = 7f
    };

    [Header("Spawn Area")]
    [SerializeField, Min(0f)] private float spawnOffset = 1f;
    [SerializeField, Range(0f, 0.45f)] private float horizontalViewportMargin = 0.08f;

    private Coroutine spawnRoutine;

    private void Awake()
    {
        if (gameplayCamera == null)
        {
            gameplayCamera = Camera.main;
        }
    }

    private void OnEnable()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            DifficultySettings settings = GetCurrentSettings();

            if (gameManager != null && !gameManager.IsGameOver)
            {
                SpawnItem(settings);
            }

            yield return new WaitForSeconds(Mathf.Max(0.1f, settings.spawnInterval));
        }
    }

    private void SpawnItem(DifficultySettings settings)
    {
        if (gameplayCamera == null || gameManager == null)
        {
            return;
        }

        bool spawnObstacle = Random.value < settings.obstacleChance;
        GameObject selectedPrefab = spawnObstacle ? obstaclePrefab : waterDropPrefab;
        if (selectedPrefab == null)
        {
            selectedPrefab = spawnObstacle ? waterDropPrefab : obstaclePrefab;
        }

        if (selectedPrefab == null)
        {
            return;
        }

        float viewportX = Random.Range(horizontalViewportMargin, 1f - horizontalViewportMargin);
        float cameraDistance = Mathf.Abs(gameplayCamera.transform.position.z - transform.position.z);
        Vector3 spawnPosition = gameplayCamera.ViewportToWorldPoint(new Vector3(viewportX, 1f, cameraDistance));
        spawnPosition.y += spawnOffset;
        spawnPosition.z = transform.position.z;

        GameObject itemObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);
        FallingItem fallingItem = itemObject.GetComponent<FallingItem>();
        if (fallingItem == null)
        {
            Debug.LogWarning($"O prefab {selectedPrefab.name} não tem FallingItem.", selectedPrefab);
            Destroy(itemObject);
            return;
        }

        float lowSpeed = Mathf.Min(settings.minimumFallSpeed, settings.maximumFallSpeed);
        float highSpeed = Mathf.Max(settings.minimumFallSpeed, settings.maximumFallSpeed);
        fallingItem.Initialize(gameManager, Random.Range(lowSpeed, highSpeed));
    }

    private DifficultySettings GetCurrentSettings()
    {
        return difficulty switch
        {
            Difficulty.Medium => medium,
            Difficulty.Hard => hard,
            _ => easy
        };
    }
}
