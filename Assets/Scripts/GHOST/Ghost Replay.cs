/*using UnityEngine;
using System.IO;

public class GhostReplay : MonoBehaviour
{
    public string fileName = "ghost.json";

    public float moveSpeed = 15f;

    private GhostData ghostData;

    private int currentFrame;

    void Start()
    {
        LoadGhost();
    }

    void FixedUpdate()
    {
        if (ghostData == null)
            return;

        if (currentFrame >= ghostData.frames.Count)
            return;

        GhostFrame frame =
            ghostData.frames[currentFrame];

        Vector3 targetPosition =
            frame.GetPosition();

        // MOVE
        transform.position =
            Vector3.Lerp(
                transform.position,
                targetPosition,
                moveSpeed
                * Time.fixedDeltaTime
            );

        // ROTATE
        Quaternion targetRotation =
            Quaternion.Euler(
                0f,
                frame.rotY,
                0f
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                moveSpeed
                * Time.fixedDeltaTime
            );

        currentFrame++;
    }

    void LoadGhost()
    {
        string path =
            Application.persistentDataPath
            + "/"
            + fileName;

        if (!File.Exists(path))
        {
            Debug.Log("NO GHOST FOUND");

            return;
        }

        string json =
            File.ReadAllText(path);

        ghostData =
            JsonUtility.FromJson<GhostData>(
                json
            );

        Debug.Log("Ghost Loaded");
    }
}*/

using UnityEngine;
using System.IO;

public class GhostReplay : MonoBehaviour
{
    [Header("Ghost File")]

    // WHICH GHOST TO LOAD
    public string fileName =
        "ghost1.json";

    [Header("Replay")]

    public float moveSpeed =
        15f;

    private GhostData ghostData;

    private int currentFrame = 0;

    void Start()
    {
        LoadGhost();
    }

    void FixedUpdate()
    {
        if (ghostData == null)
            return;

        if (
            currentFrame >=
            ghostData.frames.Count
        )
            return;

        GhostFrame frame =
            ghostData.frames[currentFrame];

        Vector3 targetPosition =
            frame.GetPosition();

        // MOVE
        transform.position =
            Vector3.Lerp(
                transform.position,
                targetPosition,
                moveSpeed
                * Time.fixedDeltaTime
            );

        // ROTATE
        Quaternion targetRotation =
            Quaternion.Euler(
                0f,
                frame.rotY,
                0f
            );

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                moveSpeed
                * Time.fixedDeltaTime
            );

        currentFrame++;
    }

    void LoadGhost()
    {
        string path =
            Application.persistentDataPath
            + "/"
            + fileName;

        if (!File.Exists(path))
        {
            Debug.Log(
                "NO GHOST FILE FOUND"
            );

            return;
        }

        string json =
            File.ReadAllText(path);

        ghostData =
            JsonUtility.FromJson<GhostData>(
                json
            );

        Debug.Log(
            "Ghost Loaded: "
            + fileName
        );
    }
}