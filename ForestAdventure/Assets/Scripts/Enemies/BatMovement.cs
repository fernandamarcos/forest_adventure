using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public float speed = 3f; // Velocidad del murci�lago
    public float minX = -6f; // Valor m�nimo en X hasta donde el murci�lago puede volar
    public float maxX = 45f;  // Valor m�ximo en X hasta donde el murci�lago puede volar
    public float idleThreshold = 0.1f; // Umbral para considerar si el murci�lago est� lo suficientemente cerca del destino para cambiar de direcci�n

    private Vector2 startPosition;
    private bool movingRight = true; // Para controlar la direcci�n del movimiento
    private Animator animator;

    void Start()
    {
        // Guardamos la posici�n inicial del murci�lago
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movemos al murci�lago
        MoveBat();
    }

    void MoveBat()
    {
        // Determinar la posici�n objetivo dependiendo de la direcci�n
        float targetX = movingRight ? maxX : minX;

        // Mover al murci�lago hacia el objetivo en el eje X
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), speed * Time.deltaTime);

        // Activar la animaci�n de vuelo mientras el murci�lago se mueve
        animator.SetBool("flying", true);

        // Cambiar de direcci�n cuando el murci�lago est� lo suficientemente cerca del objetivo
        if (Mathf.Abs(transform.position.x - targetX) < idleThreshold)
        {
            movingRight = !movingRight;
            RotateBat();
        }
    }

    // Rotar el murci�lago seg�n la direcci�n en la que est� volando
    void RotateBat()
    {
        if (movingRight)
        {
            transform.localScale = new Vector3(5, 4, 1); // Rotaci�n a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-5, 4, 1); // Rotaci�n a la izquierda
        }
    }
}
