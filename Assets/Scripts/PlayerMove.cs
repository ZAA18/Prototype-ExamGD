using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float startSpeed = 5f;
    public float maxSpeed = 20f;
    public float accelerationRate = 6f;

    // Air movement strength
    public float airControl = 0.5f;

    // Slows movement when no input
    public float groundDrag = 4f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundDistance = 0.6f;

    private bool isGrounded;

    float horizontalInput;
    float verticalInput;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = startSpeed;
    }

    void Update()
    {
        // INPUT
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // GROUND CHECK
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundDistance,
            groundMask
        );

        // DRAG
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0.5f;
        }

        // CHECK IF PLAYER IS MOVING
        bool isMoving =
            Mathf.Abs(horizontalInput) > 0.1f ||
            Mathf.Abs(verticalInput) > 0.1f;

        // SPEED INCREASE
        if (isMoving)
        {
            currentSpeed += accelerationRate * Time.deltaTime;

            // LIMIT MAX SPEED
            currentSpeed = Mathf.Clamp(
                currentSpeed,
                startSpeed,
                maxSpeed
            );
        }
        else
        {
            // RESET SPEED WHEN PLAYER STOPS
            currentSpeed = startSpeed;
        }

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // CAMERA DIRECTION
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // MOVE DIRECTION
        Vector3 moveDirection =
            (forward * verticalInput + right * horizontalInput).normalized;

        // AIR CONTROL
        float controlMultiplier = isGrounded ? 1f : airControl;

        // APPLY FORCE
        rb.AddForce(
            moveDirection * currentSpeed * controlMultiplier,
            ForceMode.Acceleration
        );

        // LIMIT HORIZONTAL SPEED
        Vector3 flatVelocity =
            new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > currentSpeed)
        {
            Vector3 limitedVelocity =
                flatVelocity.normalized * currentSpeed;

            rb.linearVelocity = new Vector3(
                limitedVelocity.x,
                rb.linearVelocity.y,
                limitedVelocity.z
            );
        }
    }

    void Jump()

    {
        //checking whether we are currently grounded

        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        // checking if we are up(Jump)
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);

        }
    }

    /*void Jump()
    {
        // CHECK IF GROUNDED
        float height = GetComponent<Collider>().bounds.size.y;

        bool grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            (height / 2) + 0.1f,
            groundMask
        );

        // JUMP
        if (grounded)
        {
            rb.AddForce(Vector3.up * jumpForce,
                ForceMode.Impulse);
        }
    }*/
}




/*using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 12f;

    // How fast player changes direction
    public float acceleration = 20f;
    
    // Air movement strength
    public float airControl = 0.5f;

    // Slows movement when no input
    public float groundDrag = 4f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundDistance = 0.6f;

    private bool isGrounded;

    float horizontalInput;
    float verticalInput;

    void Update()
    {
        // INPUT
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // GROUND CHECK
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundDistance,
            groundMask
        );

        // DRAG
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0.5f;
        }

        // JUMP
        /* if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
         {
             Jump();
         }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // CAMERA DIRECTION
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // MOVE DIRECTION
        Vector3 moveDirection =
            (forward * verticalInput + right * horizontalInput).normalized;

        // DIFFERENT CONTROL IN AIR
        float controlMultiplier = isGrounded ? 1f : 0.5f;

        // APPLY FORCE
        rb.AddForce(moveDirection * moveSpeed * controlMultiplier,
            ForceMode.Acceleration);

        // LIMIT HORIZONTAL SPEED
        Vector3 flatVelocity =
            new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        float maxSpeed = 10f;

        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;

            rb.linearVelocity = new Vector3(
                limitedVelocity.x,
                rb.linearVelocity.y,
                limitedVelocity.z
            );
        }
    }

    /*void MovePlayer()
    {
        // CAMERA DIRECTION
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // MOVEMENT DIRECTION
        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // CURRENT VELOCITY
        Vector3 targetVelocity = moveDirection * moveSpeed;

        // KEEP Y VELOCITY
        targetVelocity.y = rb.linearVelocity.y;

        // SMOOTH MOVEMENT
        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            targetVelocity,
            10f * Time.fixedDeltaTime
        );
    }


    void Jump()

    {
        //checking whether we are currently grounded

        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        // checking if we are up(Jump)
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);

        }
    }
}
*/