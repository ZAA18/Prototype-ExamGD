using UnityEngine;
using System.IO;

public class GhostRecorder : MonoBehaviour
{
    public string fileName = "ghost.json";

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