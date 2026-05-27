/*using UnityEngine;
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
            Mathf.Max(currentWaypoint - 3, 0);

        transform.position =
            waypoints[safeIndex].transform.position
            + Vector3.up * 2f;

        rb.linearVelocity = Vector3.zero;
    }
}*/

/*using UnityEngine;
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

        // SMART SPEED CONTROL (FEELS MORE HUMAN)
        float targetSpeed = maxSpeed;

        if (currentWaypoint < waypoints.Length)
        {
            targetSpeed = waypoints[currentWaypoint].targetSpeed;
        }

        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            2f * Time.deltaTime
        );
    }

    void FixedUpdate()
    {
        if (waiting) return;

        if (currentWaypoint >= waypoints.Length)
            return;

        Transform target =
            waypoints[currentWaypoint].transform;

        // BASE DIRECTION
        Vector3 direction =
            (target.position - transform.position).normalized;

        direction.y = 0f;

        // 🔥 LOOK AHEAD (MAKES AI SMARTER)
        if (currentWaypoint + 1 < waypoints.Length)
        {
            Vector3 nextDir =
                (waypoints[currentWaypoint + 1].transform.position
                - transform.position).normalized;

            nextDir.y = 0f;

            direction = Vector3.Lerp(direction, nextDir, 0.5f);
        }

        // CONTROL
        float controlMultiplier =
            isGrounded ? 1f : airControl;

        // MOVEMENT
        Vector3 targetVelocity =
            direction * currentSpeed * controlMultiplier;

        targetVelocity.y = rb.linearVelocity.y;

        if (isGrounded)
        {
            rb.linearVelocity = Vector3.Lerp(
                rb.linearVelocity,
                targetVelocity,
                4f * Time.fixedDeltaTime
            );
        }
        else
        {
            rb.AddForce(
                direction * currentSpeed * airControl,
                ForceMode.Acceleration
            );
        }

        // ROTATION
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
                StartCoroutine(WaitRoutine(waypoint.waitTime));
                break;

            case WaypointType.Jump:
                Jump(waypoint.jumpForce);
                currentWaypoint++;
                break;
        }
    }

    IEnumerator WaitRoutine(float waitTime)
    {
        waiting = true;

        rb.linearVelocity = Vector3.zero;

        // 🔥 HUMAN-LIKE RANDOM DELAY
        float randomDelay = Random.Range(0f, 0.8f);

        yield return new WaitForSeconds(waitTime + randomDelay);

        waiting = false;
        currentWaypoint++;
    }

    void Jump(float force)
    {
        if (currentWaypoint + 1 >= waypoints.Length)
            return;

        Transform nextPoint =
            waypoints[currentWaypoint + 1].transform;

        Vector3 jumpDirection =
            (nextPoint.position - transform.position).normalized;

        jumpDirection.y = 0.5f;
        jumpDirection.Normalize();

        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

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
            Mathf.Max(currentWaypoint - 3, 0);

        transform.position =
            waypoints[safeIndex].transform.position
            + Vector3.up * 2f;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        currentWaypoint = safeIndex;
    }
}
*/

/*using UnityEngine;
using System.Collections;

public class SmartAIBall : MonoBehaviour
{
    [Header("Waypoints")]
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

    [Header("Edge Detection")]
    public float edgeCheckDistance = 2f;
    public float edgeRayHeight = 0.5f;
    public float edgeRayLength = 3f;

    [Header("Smart AI")]
    public float dangerBrakeSpeed = 6f;
    public float recoveryTurnForce = 120f;

    [Header("Respawn")]
    public float respawnHeight = 2f;

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

        // ROTATE TOWARDS TARGET
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.fixedDeltaTime
            );
        }

        // SMART EDGE DETECTION
        if (IsEdgeAhead())
        {
            // BRAKE HARD
            rb.linearVelocity = Vector3.Lerp(
                rb.linearVelocity,
                Vector3.zero,
                dangerBrakeSpeed * Time.fixedDeltaTime
            );

            // TURN AWAY FROM EDGE
            transform.Rotate(
                0f,
                recoveryTurnForce * Time.fixedDeltaTime,
                0f
            );

            return;
        }

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

    bool IsEdgeAhead()
    {
        Vector3 forwardPoint =
            transform.position
            + transform.forward * edgeCheckDistance
            + Vector3.up * edgeRayHeight;

        bool groundAhead = Physics.Raycast(
            forwardPoint,
            Vector3.down,
            edgeRayLength,
            groundMask
        );

        Debug.DrawRay(
            forwardPoint,
            Vector3.down * edgeRayLength,
            groundAhead ? Color.green : Color.red
        );

        return !groundAhead;
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

        // GIVE UPWARD BOOST
        jumpDirection.y = 0.5f;

        jumpDirection.Normalize();

        // RESET VELOCITY
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
            Mathf.Max(currentWaypoint - 3, 0);

        transform.position =
            waypoints[safeIndex].transform.position
            + Vector3.up * respawnHeight;

        rb.linearVelocity = Vector3.zero;

        rb.angularVelocity = Vector3.zero;
    }
}*/

using UnityEngine;
using System.Collections.Generic;

public class SmartAIBall : MonoBehaviour
{
    [System.Serializable]
    public class JumpData
    {
        public float speed = 30f;
        public float runway = 10f;
        public float jumpForce = 18f;
    }

    public AIWayPoint[] waypoints;

    public LayerMask groundMask;

    public float moveAcceleration = 8f;

    public float turnSpeed = 6f;

    public float groundDistance = 0.7f;

    public float jumpHeightBoost = 0.35f;

    public float respawnHeight = 2f;

    private Rigidbody rb;

    private int currentWaypoint = 0;

    private bool grounded;

    private bool planningJump;

    private bool backingUp;

    private bool sprinting;

    private bool jumping;

    private Vector3 backupTarget;

    private Vector3 lastSafePosition;

    private JumpData currentJump;

    private Dictionary<int, JumpData>
        memory =
        new Dictionary<int, JumpData>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        LoadMemory();

        lastSafePosition = transform.position;
    }

    void Update()
    {
        grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundDistance,
            groundMask
        );

        if (grounded)
        {
            lastSafePosition = transform.position;
        }
    }

    void FixedUpdate()
    {
        if (currentWaypoint >= waypoints.Length)
            return;

        AIWayPoint waypoint =
            waypoints[currentWaypoint];

        Vector3 direction =
            (
                waypoint.transform.position
                - transform.position
            ).normalized;

        direction.y = 0f;

        Rotate(direction);

        // JUMP LOGIC
        if (waypoint.waypointType ==
            WaypointType.Jump)
        {
            HandleJumpWaypoint(
                waypoint,
                direction
            );

            return;
        }

        // NORMAL MOVE
        Move(direction, 14f);

        float distance =
            Vector3.Distance(
                transform.position,
                waypoint.transform.position
            );

        if (distance < 3f)
        {
            currentWaypoint++;
        }
    }

    void HandleJumpWaypoint(
        AIWayPoint waypoint,
        Vector3 direction
    )
    {
        int jumpID = currentWaypoint;

        if (!memory.ContainsKey(jumpID))
        {
            memory.Add(
                jumpID,
                new JumpData()
            );
        }

        currentJump = memory[jumpID];

        // STEP 1
        // CREATE JUMP PLAN ONCE
        if (!planningJump &&
            !backingUp &&
            !sprinting &&
            !jumping)
        {
            planningJump = true;

            backupTarget =
                transform.position
                - direction
                * currentJump.runway;
        }

        // STEP 2
        // BACK UP
        if (planningJump)
        {
            backingUp = true;

            planningJump = false;
        }

        if (backingUp)
        {
            Vector3 backupDirection =
                (
                    backupTarget
                    - transform.position
                ).normalized;

            backupDirection.y = 0f;

            Move(
                backupDirection,
                currentJump.speed
            );

            float backupDistance =
                Vector3.Distance(
                    transform.position,
                    backupTarget
                );

            if (backupDistance < 2f)
            {
                backingUp = false;

                sprinting = true;
            }

            return;
        }

        // STEP 3
        // FULL SPEED RUN
        if (sprinting)
        {
            Move(
                direction,
                currentJump.speed
            );

            float edgeDistance =
                Vector3.Distance(
                    transform.position,
                    waypoint.transform.position
                );

            // DYNAMIC JUMP TIMING
            if (
                rb.linearVelocity.magnitude
                >= currentJump.speed * 0.9f
                &&
                edgeDistance < 4f
            )
            {
                sprinting = false;

                jumping = true;

                Jump(
                    direction,
                    currentJump.jumpForce
                );

                currentWaypoint++;
            }

            return;
        }

        // STEP 4
        // SUCCESSFUL LANDING
        if (jumping)
        {
            if (grounded)
            {
                jumping = false;

                LearnSuccess(jumpID);
            }
        }
    }

    void Move(
        Vector3 direction,
        float speed
    )
    {
        Vector3 targetVelocity =
            direction * speed;

        targetVelocity.y =
            rb.linearVelocity.y;

        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            targetVelocity,
            moveAcceleration
            * Time.fixedDeltaTime
        );
    }

    void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed
                * Time.fixedDeltaTime
            );
    }

    void Jump(
        Vector3 direction,
        float force
    )
    {
        rb.linearVelocity =
            new Vector3(
                rb.linearVelocity.x,
                0f,
                rb.linearVelocity.z
            );

        Vector3 jumpDirection =
            direction;

        // LOW ARC
        jumpDirection.y =
            jumpHeightBoost;

        jumpDirection.Normalize();

        rb.AddForce(
            jumpDirection * force,
            ForceMode.Impulse
        );
    }

    void LearnSuccess(int jumpID)
    {
        JumpData data =
            memory[jumpID];

        // SLIGHTLY OPTIMIZE
        data.speed =
            Mathf.Max(
                24f,
                data.speed - 0.2f
            );

        SaveMemory();
    }

    void LearnFailure(int jumpID)
    {
        if (!memory.ContainsKey(jumpID))
            return;

        JumpData data =
            memory[jumpID];

        // IMPROVE AFTER FAILING
        data.speed += 2f;

        data.runway += 1.5f;

        data.jumpForce += 0.5f;

        data.speed =
            Mathf.Clamp(
                data.speed,
                25f,
                60f
            );

        data.runway =
            Mathf.Clamp(
                data.runway,
                8f,
                30f
            );

        data.jumpForce =
            Mathf.Clamp(
                data.jumpForce,
                18f,
                28f
            );

        SaveMemory();
    }

    void Respawn()
    {
        transform.position =
            lastSafePosition
            + Vector3.up
            * respawnHeight;

        rb.linearVelocity = Vector3.zero;

        rb.angularVelocity = Vector3.zero;

        planningJump = false;

        backingUp = false;

        sprinting = false;

        jumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            LearnFailure(
                Mathf.Max(
                    currentWaypoint - 1,
                    0
                )
            );

            Respawn();
        }
    }

    void SaveMemory()
    {
        foreach (var item in memory)
        {
            int id = item.Key;

            PlayerPrefs.SetFloat(
                "Speed_" + id,
                item.Value.speed
            );

            PlayerPrefs.SetFloat(
                "Runway_" + id,
                item.Value.runway
            );

            PlayerPrefs.SetFloat(
                "Force_" + id,
                item.Value.jumpForce
            );
        }

        PlayerPrefs.Save();
    }

    void LoadMemory()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            JumpData data =
                new JumpData();

            data.speed =
                PlayerPrefs.GetFloat(
                    "Speed_" + i,
                    30f
                );

            data.runway =
                PlayerPrefs.GetFloat(
                    "Runway_" + i,
                    10f
                );

            data.jumpForce =
                PlayerPrefs.GetFloat(
                    "Force_" + i,
                    18f
                );

            memory.Add(i, data);
        }
    }
}