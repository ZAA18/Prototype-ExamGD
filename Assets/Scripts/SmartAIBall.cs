using UnityEngine;
using System.Collections;

public class SmartAIBall : MonoBehaviour
{
    public AIWayPoint[] waypoints;

    [Header("Movement")]
    public float startSpeed = 5f;
    public float maxSpeed = 20f;
    public float accelerationRate = 6f;

    [Header("Turning")]
    public float turnSpeed = 5f;

    [Header("Ground")]
    public LayerMask groundMask;
    public float groundDistance = 0.6f;

    [Header("Air Control")]
    public float airControl = 0.5f;

    private Rigidbody rb;

    private int currentWaypoint = 0;

    private bool waiting = false;

    private float currentSpeed;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = startSpeed;
    }

    void Update()
    {
        // GROUND CHECK
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundDistance,
            groundMask
        );

        // SPEED BUILDUP
        currentSpeed += accelerationRate * Time.deltaTime;

        currentSpeed = Mathf.Clamp(
            currentSpeed,
            startSpeed,
            maxSpeed
        );
    }

    void FixedUpdate()
    {
        if (waiting) return;

        if (currentWaypoint >= waypoints.Length)
            return;

        Transform target =
            waypoints[currentWaypoint].transform;

        // DIRECTION
        Vector3 direction =
            (target.position - transform.position).normalized;

        direction.y = 0f;

        // CONTROL
        float controlMultiplier =
            isGrounded ? 1f : airControl;

        // TARGET VELOCITY
        Vector3 targetVelocity =
            direction * currentSpeed * controlMultiplier;

        targetVelocity.y = rb.linearVelocity.y;

        // SMOOTH MOVEMENT
        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            targetVelocity,
            4f * Time.fixedDeltaTime
        );

        // ROTATE TOWARDS TARGET
        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        // DISTANCE CHECK
        float distance =
            Vector3.Distance(
                transform.position,
                target.position
            );

        if (distance < 3f)
        {
            HandleWaypoint();
        }
    }

    void HandleWaypoint()
    {
        AIWayPoint waypoint =
            waypoints[currentWaypoint];

        switch (waypoint.waypointType)
        {
            case WaypointType.Normal:

                currentWaypoint++;

                break;

            case WaypointType.Wait:

                StartCoroutine(
                    WaitRoutine(
                        waypoint.waitTime
                    )
                );

                break;

            case WaypointType.Jump:

                Jump(
                    waypoint.jumpForce
                );

                currentWaypoint++;

                break;
        }
    }

    IEnumerator WaitRoutine(float waitTime)
    {
        waiting = true;

        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(waitTime);

        waiting = false;

        currentWaypoint++;
    }

    /* void Jump(float force)
     {
         if (isGrounded)
         {
             rb.AddForce(
                 Vector3.up * force,
                 ForceMode.Impulse
             );
         }
     }*/

    void Jump(float force)
    {
        // CHECK IF THERE IS A NEXT WAYPOINT
        if (currentWaypoint + 1 >= waypoints.Length)
            return;

        // TARGET = NEXT WAYPOINT
        Transform nextPoint =
            waypoints[currentWaypoint + 1].transform;

        // DIRECTION TO NEXT POINT
        Vector3 jumpDirection =
            (nextPoint.position - transform.position).normalized;

        // FLATTEN SLIGHTLY
        jumpDirection.y = 0.5f;

        // NORMALIZE AGAIN
        jumpDirection.Normalize();

        // RESET OLD VELOCITY
        rb.linearVelocity = Vector3.zero;

        // LAUNCH AI
        rb.AddForce(
            jumpDirection * force,
            ForceMode.Impulse
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        int safeIndex =
            Mathf.Max(currentWaypoint - 1, 0);

        transform.position =
            waypoints[safeIndex].transform.position
            + Vector3.up * 2f;

        rb.linearVelocity = Vector3.zero;
    }
}