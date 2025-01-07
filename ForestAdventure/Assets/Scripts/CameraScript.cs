using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target; // Referencia al personaje (El GameObject del jugador)
    public Vector3 offset;  // El desplazamiento relativo de la c�mara respecto al personaje
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
            // Calculamos la posici�n deseada para la c�mara, tomando el transform del target y sum�ndole el offset
            Vector3 desiredPosition = target.transform.position + offset;

            // Restringimos la c�mara a los l�mites de la escena
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Fijamos el eje Z de la c�mara a 0.10 (sin modificar el valor de X y Y)
            desiredPosition.z = -10;

            // Interpolamos suavemente para un movimiento m�s fluido de la c�mara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asignamos la nueva posici�n a la c�mara
            transform.position = smoothedPosition;
        }
    }

    void SetCameraLimits()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect; // Ancho visible de la c�mara
        float halfHeight = cam.orthographicSize; // Altura visible de la c�mara

        minX = 2.2f;  // Limite m�nimo de X
        maxX = 30f;   // Limite m�ximo de X
        minY = 0f;   // Limite m�nimo de Y
        maxY = 2.6f;    // Limite m�ximo de Y
    }
}
