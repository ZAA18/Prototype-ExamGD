using UnityEngine;

public class AIWayPoint : MonoBehaviour
{
  
    public WaypointType waypointType;

    [Header("Wait")]
    public float waitTime = 1f;

    [Header("Jump")]
    public float jumpForce = 7f;
}

public enum WaypointType
{
    Normal,
    Wait,
    Jump
}

