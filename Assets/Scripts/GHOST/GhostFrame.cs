/*using UnityEngine;

[System.Serializable]
public class GhostFrame
{
    public float posX;
    public float posY;
    public float posZ;

    public float rotY;

    public GhostFrame(
        Vector3 position,
        float rotationY
    )
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;

        rotY = rotationY;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(
            posX,
            posY,
            posZ
        );
    }
}*/

using UnityEngine;

[System.Serializable]
public class GhostFrame
{
    public float posX;
    public float posY;
    public float posZ;

    public float rotY;

    public GhostFrame(
        Vector3 position,
        float rotationY
    )
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;

        rotY = rotationY;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(
            posX,
            posY,
            posZ
        );
    }
}