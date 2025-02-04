using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
    public float jumpforce = 3.0f;
    public bool isOnGround = true;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    private Animator squidAnimator; // Reference to the Animator

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        squidAnimator = GetComponent<Animator>(); // Get the Animator component attached to the squid
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        // Trigger movement animation
        bool isMoving = Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(forwardInput) > 0;
        squidAnimator.SetBool("isMoving", isMoving); // Set "isMoving" parameter in Animator

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isOnGround = false;
            squidAnimator.SetTrigger("Jump"); // Trigger the "Jump" animation
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}