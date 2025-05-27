using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // el objeto a seguir, como el jugador

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target != null)
        {
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}