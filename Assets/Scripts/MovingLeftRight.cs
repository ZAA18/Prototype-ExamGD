using UnityEngine;

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
