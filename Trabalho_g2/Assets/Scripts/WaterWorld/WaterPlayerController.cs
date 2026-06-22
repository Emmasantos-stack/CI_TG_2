using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class WaterPlayerController : MonoBehaviour
{
    [SerializeField, Min(0f)] private float moveSpeed = 8f;
    [SerializeField, Min(0f)] private float horizontalMargin = 0.25f;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Camera gameplayCamera;
    [SerializeField] private WaterGameManager gameManager;

    private Rigidbody2D body;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        if (gameplayCamera == null)
        {
            gameplayCamera = Camera.main;
        }
    }

    private void OnEnable()
    {
        if (moveAction != null && moveAction.action != null)
        {
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveAction != null && moveAction.action != null)
        {
            moveAction.action.Disable();
        }
    }

    private void Update()
    {
        horizontalInput = ReadHorizontalInput();
    }

    private void FixedUpdate()
    {
        if (body == null || gameplayCamera == null || (gameManager != null && gameManager.IsGameOver))
        {
            return;
        }

        Vector2 position = body.position;
        position.x += horizontalInput * moveSpeed * Time.fixedDeltaTime;

        float cameraDistance = Mathf.Abs(gameplayCamera.transform.position.z - transform.position.z);
        float leftLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, cameraDistance)).x
            + horizontalMargin;
        float rightLimit = gameplayCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, cameraDistance)).x
            - horizontalMargin;

        position.x = Mathf.Clamp(position.x, leftLimit, rightLimit);
        body.MovePosition(position);
    }

    private float ReadHorizontalInput()
    {
        if (moveAction != null && moveAction.action != null)
        {
            return Mathf.Clamp(moveAction.action.ReadValue<Vector2>().x, -1f, 1f);
        }

        if (Keyboard.current != null)
        {
            float left = Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed ? -1f : 0f;
            float right = Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed ? 1f : 0f;
            return left + right;
        }

#if ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetAxisRaw("Horizontal");
#else
        return 0f;
#endif
    }
}
