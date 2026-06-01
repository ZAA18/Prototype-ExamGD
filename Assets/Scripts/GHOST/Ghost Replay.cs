
using UnityEngine;
using System.IO;

public class GhostReplay : MonoBehaviour
{
    [Header("Ghost File")]
    public string fileName = "ghost1.json";

    private GhostData ghostData;
    private int currentFrame = 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LoadGhost();
    }

    void FixedUpdate()
    {
        if (ghostData == null) return;
        if (currentFrame >= ghostData.frames.Count) return;

        GhostFrame frame = ghostData.frames[currentFrame];

        Vector3 targetPosition = frame.GetPosition();
        Vector3 targetVelocity = frame.GetVelocity();

        // ✔ EXACT POSITION MATCH (no smoothing)
        rb.MovePosition(targetPosition);

        // ✔ RESTORE REAL MOVEMENT SPEED (VERY IMPORTANT)
        rb.linearVelocity = targetVelocity;

        // rotation (optional smoothing is okay for visuals)
        /*  Quaternion targetRotation = Quaternion.Euler(0f, frame.rotY, 0f);
          rb.MoveRotation(targetRotation);
        */

        /* Quaternion targetRotation = Quaternion.Euler(
     frame.rotX,
     frame.rotY,
     frame.rotZ
 );


         rb.MoveRotation(targetRotation);
        */

        rb.MoveRotation(
    Quaternion.Euler(
        frame.rotX,
        frame.rotY,
        frame.rotZ
    )
);

        currentFrame++;
    }

    void LoadGhost()
    {
        string path = Application.persistentDataPath + "/" + fileName;

        if (!File.Exists(path))
        {
            Debug.Log("NO GHOST FILE FOUND");
            return;
        }

        string json = File.ReadAllText(path);
        ghostData = JsonUtility.FromJson<GhostData>(json);

        Debug.Log("Ghost Loaded: " + fileName);
    }
}