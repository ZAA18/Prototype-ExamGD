
using UnityEngine;

[System.Serializable]
public class GhostFrame
{
    public float posX;
    public float posY;
    public float posZ;

    public float rotY;

    public float velX;
    public float velY;
    public float velZ;
    public float rotX;
    public float rotZ;

    /*  public GhostFrame(Vector3 position, float rotationY, Vector3 velocity)
      {
          posX = position.x;
          posY = position.y;
          posZ = position.z;

          rotY = rotationY;

          velX = velocity.x;
          velY = velocity.y;
          velZ = velocity.z;
      }
    */

    public GhostFrame(Vector3 position, Vector3 rotation, Vector3 velocity)
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;

        rotX = rotation.x;
        rotY = rotation.y;
        rotZ = rotation.z;

        velX = velocity.x;
        velY = velocity.y;
        velZ = velocity.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(posX, posY, posZ);
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(velX, velY, velZ);
    }
}