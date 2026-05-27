/*using UnityEngine;
using System.IO;

public class GhostRecorder : MonoBehaviour
{
    [Header ("Ghost Save")]
    public string fileName = "ghost1.json";
    public bool overwriteExisting = false;

    public float recordInterval = 0.02f;

    private float timer;

    private GhostData ghostData =
        new GhostData();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= recordInterval)
        {
            timer = 0f;

            RecordFrame();
        }
    }

    void RecordFrame()
    {
        GhostFrame frame =
            new GhostFrame(
                transform.position,
                transform.eulerAngles.y
            );

        ghostData.frames.Add(frame);
    }

    public void SaveGhost()
    {
        string json =
            JsonUtility.ToJson(
                ghostData,
                true
            );

        string path =
            Application.persistentDataPath
            + "/"
            + fileName;

        /*File.WriteAllText(
            path,
            json
        );
        

        if (File.Exists(path) &&
    !overwriteExisting)
        {
            Debug.Log(
                "Ghost already exists. Not overwriting."
            );

            return;
        }

        Debug.Log(
            "Ghost Saved To: "
            + path
        );
    }

    private void //OnApplicationQuit()
    {
        SaveGhost();
    }
}
*/

using UnityEngine;
using System.IO;

public class GhostRecorder : MonoBehaviour
{
    [Header("Ghost Save")]

    // NAME OF FILE
    public string fileName =
        "ghost1.json";

    // PREVENT OVERWRITING
    public bool overwriteExisting =
        false;

    [Header("Recording")]

    public float recordInterval =
        0.02f;

    private float timer;

    private GhostData ghostData =
        new GhostData();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= recordInterval)
        {
            timer = 0f;

            RecordFrame();
        }
    }

    void RecordFrame()
    {
        GhostFrame frame =
            new GhostFrame(
                transform.position,
                transform.eulerAngles.y
            );

        ghostData.frames.Add(frame);
    }

    public void SaveGhost()
    {
        string json =
            JsonUtility.ToJson(
                ghostData,
                true
            );

        string path =
            Application.persistentDataPath
            + "/"
            + fileName;

        // DON'T OVERWRITE
        if (
            File.Exists(path)
            &&
            !overwriteExisting
        )
        {
            Debug.Log(
                "Ghost already exists. Save blocked."
            );

            return;
        }

        File.WriteAllText(
            path,
            json
        );

        Debug.Log(
            "Ghost Saved To: "
            + path
        );
    }

    private void OnApplicationQuit()
    {
        SaveGhost();
    }
}

