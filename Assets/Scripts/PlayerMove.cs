using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 5;
    public float jumpForce = 7f;

    [Header(" Camera ")]
    public Transform cameraTransform;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundDistance = 0.6f;

    private bool isGrounded;

    float horizontalInput;
    float verticalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //check ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);

        //Check Ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void FixedUpdate()
    {
        //Camera Direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Remove Y so player doesnt fly
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //Movement Direction
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        //Apply force for rolling effect
        rb.AddForce(moveDirection * moveSpeed);
    }
}
