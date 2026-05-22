using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 200f;

    float xRotation = 20f;
    float yRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    { 
        //Follow Player
        transform.position = player.position;

        //Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis ("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation += mouseY;

        //Limit Up/ Down Lock
        xRotation = Mathf.Clamp(xRotation, -30f, 60f);

        //Rotate camera Holder
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
