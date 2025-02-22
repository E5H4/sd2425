using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    [Space(20)]
    [Header("Mouse Look Settings :")]
    public Vector2 clampInDegrees = new Vector2(360, 180);
    public CursorLockMode lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;
    public GameObject characterBody;

    [Space(20)]
    [Header("Camera Move Settings :")]
    public float acceleration = 1.0f; // Speed increase rate
    public float maxSpeed = 5; // Maximum movement speed
    public float dampingSpeed = 0.2f; // How quickly the movement slows down

    // Key bindings for movement
    public KeyCode fwdKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode backKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;

    private float speedX = 0, speedZ = 0; // Movement speed variables

    void Start()
    {
        // Set initial target direction based on the camera's starting orientation
        targetDirection = transform.localRotation.eulerAngles;

        // If a character body is assigned, set its initial target rotation
        if (characterBody)
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
    }

    void Update()
    {
        // Lock the cursor to the screen if enabled
        Cursor.lockState = lockCursor;

        // Convert target directions into Quaternion rotations
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse movement input
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Apply sensitivity and smoothing to the mouse movement
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Smooth out mouse movement using interpolation
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Accumulate absolute mouse movement
        _mouseAbsolute += _smoothMouse;

        // Clamp horizontal rotation if necessary
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Apply vertical rotation
        var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
        transform.localRotation = xRotation;

        // Clamp vertical rotation if necessary
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        // Apply horizontal rotation
        transform.localRotation *= targetOrientation;

        // If the camera is attached to a character body, rotate the body
        if (characterBody)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, characterBody.transform.up);
            characterBody.transform.localRotation = yRotation;
            characterBody.transform.localRotation *= targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
    }

    void FixedUpdate()
    {
        bool isMoving = false; // Track if the player is pressing movement keys

        // Handle movement in the X (left/right) direction
        if (Input.GetKey(rightKey))
        {
            speedX += acceleration * Time.deltaTime;
            isMoving = true;
        }
        else if (Input.GetKey(leftKey))
        {
            speedX -= acceleration * Time.deltaTime;
            isMoving = true;
        }

        // Handle movement in the Z (forward/backward) direction
        if (Input.GetKey(backKey))
        {
            speedZ -= acceleration * Time.deltaTime;
            isMoving = true;
        }
        else if (Input.GetKey(fwdKey))
        {
            speedZ += acceleration * Time.deltaTime;
            isMoving = true;
        }

        // If no movement keys are pressed, stop movement immediately
        if (!isMoving)
        {
            speedX = 0;
            speedZ = 0;
        }

        // Clamp speeds to the max allowed movement speed
        speedX = Mathf.Clamp(speedX, -maxSpeed * Time.deltaTime, maxSpeed * Time.deltaTime);
        speedZ = Mathf.Clamp(speedZ, -maxSpeed * Time.deltaTime, maxSpeed * Time.deltaTime);

        // Apply movement to the player's position
        transform.position = transform.TransformPoint(new Vector3(speedX, 0, speedZ));
    }
}