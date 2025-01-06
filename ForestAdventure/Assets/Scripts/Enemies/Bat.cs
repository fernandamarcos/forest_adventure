using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bat : Enemy
{
    public float speed = 3f; // Velocidad del murciélago
    public float minX = -6f; // Valor mínimo en X hasta donde el murciélago puede volar
    public float maxX = 45f;  // Valor máximo en X hasta donde el murciélago puede volar
    public float idleThreshold = 0.1f; // Umbral para considerar si el murciélago está lo suficientemente cerca del destino para cambiar de dirección

    private Vector2 startPosition;
    private bool movingRight = true; // Para controlar la dirección del movimiento
    private Animator animator;
    private Rigidbody2D rb;

    protected override void Start()
    {
        // Guardamos la posición inicial del murciélago
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    protected override void Update()
    {
        // Movemos al murciélago
        MoveBat();
    }

    void MoveBat()
    {
        // Determinar la posición objetivo dependiendo de la dirección
        float targetX = movingRight ? maxX : minX;

        // Mover al murciélago hacia el objetivo en el eje X
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), speed * Time.deltaTime);

        // Activar la animación de vuelo mientras el murciélago se mueve
        animator.SetBool("flying", true);

        // Cambiar de dirección cuando el murciélago esté lo suficientemente cerca del objetivo
        if (Mathf.Abs(transform.position.x - targetX) < idleThreshold)
        {
            movingRight = !movingRight;
            RotateBat();
        }
    }

    // Rotar el murciélago según la dirección en la que está volando
    void RotateBat()
    {
        if (movingRight)
        {
            transform.localScale = new Vector3(5, 4, 1); // Rotación a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-5, 4, 1); // Rotación a la izquierda
        }
    }

}
