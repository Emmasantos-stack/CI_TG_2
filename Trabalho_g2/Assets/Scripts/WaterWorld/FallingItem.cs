using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FallingItem : MonoBehaviour
{
    public enum ItemType
    {
        WaterDrop,
        Obstacle
    }

    [SerializeField] private ItemType itemType = ItemType.WaterDrop;
    [SerializeField, Min(1)] private int scoreValue = 10;
    [SerializeField, Min(1)] private int damageValue = 1;
    [SerializeField, Min(0f)] private float defaultFallSpeed = 4f;
    [SerializeField] private Camera gameplayCamera;
    [SerializeField, Min(0f)] private float destroyOffset = 1f;

    private Rigidbody2D body;
    private WaterGameManager gameManager;
    private float fallSpeed;
    private bool consumed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        fallSpeed = defaultFallSpeed;

        if (gameplayCamera == null)
        {
            gameplayCamera = Camera.main;
        }

        if (body != null)
        {
            body.gravityScale = 0f;
            body.freezeRotation = true;
        }
    }

    private void Start()
    {
        ApplyVelocity();
    }

    private void FixedUpdate()
    {
        if (gameManager != null && gameManager.IsGameOver)
        {
            if (body != null)
            {
                body.linearVelocity = Vector2.zero;
            }
            return;
        }

        ApplyVelocity();

        if (gameplayCamera == null)
        {
            return;
        }

        float cameraDistance = Mathf.Abs(gameplayCamera.transform.position.z - transform.position.z);
        float bottomEdge = gameplayCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, cameraDistance)).y;
        if (transform.position.y < bottomEdge - destroyOffset)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(WaterGameManager manager, float speed)
    {
        gameManager = manager;
        fallSpeed = speed > 0f ? speed : defaultFallSpeed;
        ApplyVelocity();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (consumed || gameManager == null || gameManager.IsGameOver)
        {
            return;
        }

        WaterPlayerController player = other.GetComponentInParent<WaterPlayerController>();
        if (player == null)
        {
            return;
        }

        consumed = true;

        if (itemType == ItemType.WaterDrop)
        {
            gameManager.AddScore(scoreValue);
        }
        else
        {
            gameManager.LoseLife(damageValue);
        }

        Destroy(gameObject);
    }

    private void ApplyVelocity()
    {
        if (body != null)
        {
            body.linearVelocity = Vector2.down * fallSpeed;
        }
    }
}
