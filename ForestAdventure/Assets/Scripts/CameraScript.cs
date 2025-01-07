using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target; // Referencia al personaje (El GameObject del jugador)
    public Vector3 offset;  // El desplazamiento relativo de la cámara respecto al personaje
    public float smoothSpeed = 0.125f; // Velocidad de suavizado

    private Camera mainCamera;
    private float targetSize = 8f;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        SetCameraLimits();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculamos la posición deseada para la cámara, tomando el transform del target y sumándole el offset
            Vector3 desiredPosition = target.transform.position + offset;

            // Restringimos la cámara a los límites de la escena
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Fijamos el eje Z de la cámara a 0.10 (sin modificar el valor de X y Y)
            desiredPosition.z = -10;

            // Interpolamos suavemente para un movimiento más fluido de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asignamos la nueva posición a la cámara
            transform.position = smoothedPosition;
        }
    }

    void SetCameraLimits()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect; // Ancho visible de la cámara
        float halfHeight = cam.orthographicSize; // Altura visible de la cámara

        minX = 2.2f;  // Limite mínimo de X
        maxX = 30f;   // Limite máximo de X
        minY = 0f;   // Limite mínimo de Y
        maxY = 2.6f;    // Limite máximo de Y
    }
}
