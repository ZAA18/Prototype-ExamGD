using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 5f;

    public float moveSpeed = 2f;

    public bool moveHorizontally = true;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Creates:
        // Middle -> Right -> Middle -> Left -> Middle

        float movement = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        if (moveHorizontally)
        {
            transform.position = startPosition + new Vector3(movement, 0, 0);
        }
        else
        {
            transform.position = startPosition + new Vector3(0, movement, 0);
        }
    }
}



/*using UnityEngine;

public class MovingLeftRight : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    public bool moveHorizontally = true;

    private Vector3 startPosition;

    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float movement =
            Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        Vector3 targetPosition;

        if (moveHorizontally)
        {
            targetPosition =
                startPosition + new Vector3(movement, 0, 0);
        }
        else
        {
            targetPosition =
                startPosition + new Vector3(0, movement, 0);
        }

        rb.MovePosition(targetPosition);
    }
}
*/
