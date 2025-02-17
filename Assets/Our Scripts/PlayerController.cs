using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Check if the current scene is "SampleScene"
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            moveSpeed = 1000f; // Change speed to 1000 if in Lobby
        }
        if (SceneManager.GetActiveScene().name == "HypoglycemicShock")
        {
            moveSpeed = 25f; // Change speed to 1000 if in HypoglycemicShock
        }
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Move the character with WASD
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow keys

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;
        rb.MovePosition(transform.position + move * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}