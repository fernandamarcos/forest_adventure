using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al personaje
    public Vector3 offset;   // Desplazamiento relativo entre la cámara y el personaje
    public float smoothSpeed = 0.125f; // Velocidad de seguimiento

    private Camera mainCamera;  // Referencia a la cámara principal
    private float targetSize = 8f;  // Nuevo tamaño de la cámara

    private float minX, maxX, minY, maxY; // Límites de la cámara

    void Start()
    {
        // Configura los límites de la cámara en función de la escena
        SetCameraLimits();
       
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición deseada de la cámara
            Vector3 desiredPosition = target.position + offset;

            // Restringe la cámara a los límites de la escena
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Interpolación suave para el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asigna la nueva posición a la cámara
            transform.position = smoothedPosition;
        }
    }

    void SetCameraLimits()
    {
        // Obtén los límites de la escena (con base en el tamaño del mundo/escena)
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;  // Ancho visible de la cámara
        float halfHeight = cam.orthographicSize; // Altura visible de la cámara

        minX = 2f;
        maxX = 30f;
        minY = 1f;
        maxY = 2f;
    }
}
