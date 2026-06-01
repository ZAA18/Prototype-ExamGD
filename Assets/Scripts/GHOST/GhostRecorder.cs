
using UnityEngine;
using System.IO;

public class GhostRecorder : MonoBehaviour
{
    [Header("Ghost Save")]
    public string fileName = "ghost1.json";
    public bool overwriteExisting = false;

    private GhostData ghostData = new GhostData();
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        RecordFrame();
    }

    void RecordFrame()
    {
        /*  GhostFrame frame = new GhostFrame(
              transform.position,
              transform.eulerAngles.y,
              rb.linearVelocity
          );
        */

        /*  GhostFrame frame = new GhostFrame(
      transform.position,
      transform.eulerAngles,
      rb.linearVelocity
  );
        */
        GhostFrame frame = new GhostFrame(
      transform.position,
      rb.rotation.eulerAngles,
      rb.linearVelocity
  );

        ghostData.frames.Add(frame);
    }

    public void SaveGhost()
    {
        string json = JsonUtility.ToJson(ghostData, true);

        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path) && !overwriteExisting)
        {
            Debug.Log("Ghost already exists. Save blocked.");
            return;
        }

        File.WriteAllText(path, json);

        Debug.Log("Ghost Saved To: " + path);
    }

    private void OnApplicationQuit()
    {
        SaveGhost();
    }
}