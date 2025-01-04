using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al personaje
    public Vector3 offset;   // Desplazamiento relativo entre la c�mara y el personaje
    public float smoothSpeed = 0.125f; // Velocidad de seguimiento

    private float minX, maxX, minY, maxY; // L�mites de la c�mara

    void Start()
    {
        // Configura los l�mites de la c�mara en funci�n de la escena
        SetCameraLimits();
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posici�n deseada de la c�mara
            Vector3 desiredPosition = target.position + offset;

            // Restringe la c�mara a los l�mites de la escena
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Interpolaci�n suave para el movimiento de la c�mara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asigna la nueva posici�n a la c�mara
            transform.position = smoothedPosition;
        }
    }

    void SetCameraLimits()
    {
        // Obt�n los l�mites de la escena (con base en el tama�o del mundo/escena)
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;  // Ancho visible de la c�mara
        float halfHeight = cam.orthographicSize; // Altura visible de la c�mara

        minX = -1f;
        maxX = 30f;
        minY = 1f;
        maxY = 2f;
    }
}
