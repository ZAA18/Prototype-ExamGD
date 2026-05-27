using UnityEngine;
using System.Collections;

public class SmartAIBall : MonoBehaviour
{
    public AIWayPoint[] waypoints;

    [Header("Movement")]
    public float startSpeed = 5f;
    public float maxSpeed = 20f;
    public float accelerationRate = 6f;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundDistance = 0.6f;

    [Header("Jump Settings")]
    public float jumpTime = 0.8f;
    public float maxJumpDistance = 12f;
    public float airControl = 10f;

    private Rigidbody rb;

    private int currentWaypoint = 0;
    private float currentSpeed;

    private bool isGrounded;
    private bool isJumping;

    private Transform jumpTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = startSpeed;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundDistance,
            groundMask
        );

        if (isJumping && isGrounded)
        {
            isJumping = false;
            jumpTarget = null;
        }

        currentSpeed += accelerationRate * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, startSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        if (currentWaypoint >= waypoints.Length) return;

        Transform target = waypoints[currentWaypoint].transform;

        Vector3 toTarget = target.position - transform.position;

        Vector3 flat = toTarget;
        flat.y = 0f;

        float distance = flat.magnitude;

        Vector3 direction = flat.normalized;

        // =========================
        // GROUND MOVEMENT
        // =========================
        if (isGrounded && !isJumping)
        {
            Vector3 desiredVelocity =
                direction * currentSpeed;

            Vector3 change = desiredVelocity - rb.linearVelocity;
            change.y = 0f;

            rb.AddForce(change, ForceMode.VelocityChange);
        }

        // =========================
        // AIR PARKOUR CONTROL
        // =========================
        if (isJumping && jumpTarget != null)
        {
            Vector3 to = jumpTarget.position - transform.position;
            to.y = 0f;

            Vector3 correction =
                to.normalized * airControl;

            rb.AddForce(correction, ForceMode.Acceleration);
        }

        // =========================
        // ROTATION
        // =========================
        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rot,
                8f * Time.fixedDeltaTime
            );
        }

        // =========================
        // WAYPOINT CHECK
        // =========================
        if (distance < 2.5f)
        {
            HandleWaypoint(target);
        }
    }

    void HandleWaypoint(Transform target)
    {
        AIWayPoint wp = waypoints[currentWaypoint];

        switch (wp.waypointType)
        {
            case WaypointType.Normal:
                currentWaypoint++;
                break;

            case WaypointType.Wait:
                StartCoroutine(WaitRoutine(wp.waitTime));
                break;

            case WaypointType.Jump:
                TryParkourJump(target);
                currentWaypoint++;
                break;
        }
    }

    // =========================
    // ?? PARKOUR DECISION SYSTEM
    // =========================
    void TryParkourJump(Transform target)
    {
        Vector3 start = transform.position;
        Vector3 end = target.position;

        Vector3 flat = end - start;
        flat.y = 0f;

        float distance = flat.magnitude;

        // ? too far ? refuse jump
        if (distance > maxJumpDistance)
            return;

        Vector3 velocity;
        if (!CalculateJumpSolution(start, end, jumpTime, out velocity))
            return;

        jumpTarget = target;
        isJumping = true;

        rb.linearVelocity = velocity;
    }

    // =========================
    // ?? PHYSICS SOLVER
    // =========================
    bool CalculateJumpSolution(Vector3 start, Vector3 end, float time, out Vector3 velocity)
    {
        Vector3 distance = end - start;

        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float gravity = Mathf.Abs(Physics.gravity.y);

        float vx = distanceXZ.x / time;
        float vz = distanceXZ.z / time;

        float vy = (distance.y / time) + (0.5f * gravity * time);

        velocity = new Vector3(vx, vy, vz);

        // validity check (prevents impossible jumps)
        if (float.IsNaN(vy) || float.IsInfinity(vy))
            return false;

        if (vy > 25f) return false; // too high jump (safety clamp)

        return true;
    }

    IEnumerator WaitRoutine(float time)
    {
        rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(time);
    }
}