using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target; 
    public Vector3 offset;  
    public float smoothSpeed = 0.125f; 

    private Camera mainCamera;
    private float targetSize = 8f;

    public float minX, maxX, minY, maxY;

    void Start()
    {
        SetCameraLimits();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate desired position for the camera
            Vector3 desiredPosition = target.transform.position + offset;

            // Restrict camera to the scenes limits
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            
            desiredPosition.z = -10;

            // Interpolate for smooth camera movement 
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            
            transform.position = smoothedPosition;
        }
    }

    void SetCameraLimits()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect; 
        float halfHeight = cam.orthographicSize;    }
}

}
