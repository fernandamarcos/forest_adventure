using System.Collections;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public Transform player; // El jugador
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float stopDistance = 10f; // Distancia para detenerse
    public GameObject fireballPrefab; // Prefab de la bola de fuego
    public Transform fireballSpawnPoint; // Punto de lanzamiento de la bola de fuego

    public float jumpForce = 10f; // Fuerza del salto
    public LayerMask wallLayer; // Capa de las paredes para el salto

    private Animator anim; // Referencia al Animator del wizard
    private bool isMoving = false; // Flag para saber si el wizard está moviéndose
    private bool isJumping = false; // Flag para saber si está saltando

    private float fireballCooldown = 3f; // Intervalo de lanzamiento de bolas de fuego
    private float nextFireballTime = 0f; // Tiempo hasta el siguiente lanzamiento de bola de fuego

    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private float raycastDistance = 2f; // Distancia del Raycast para detectar la pared

    void Start()
    {
        anim = GetComponent<Animator>(); // Obtén el Animator del Wizard
        rb = GetComponent<Rigidbody2D>(); // Obtén el Rigidbody2D del Wizard
    }

    void Update()
    {
        MoveTowardsPlayer();
        CheckForWallAndJump();
    }

    void MoveTowardsPlayer()
    {
        // Calcula la distancia entre el wizard y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si está más lejos que la distancia de parada, se mueve hacia el jugador
        if (distanceToPlayer > stopDistance)
        {
            isMoving = true;

            // Mover hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            // Girar según la dirección del movimiento
            if (direction.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f); // Derecha
            else if (direction.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f); // Izquierda

            // Cambiar animación a "Walking"
            anim.SetBool("isWalking", true);
        }
        else
        {
            // Si está dentro del rango de ataque o muy cerca, se detiene y lanza la bola de fuego
            isMoving = false;
            anim.SetBool("isWalking", false);

            // Lanzar bola de fuego si el tiempo lo permite
            if (Time.time > nextFireballTime)
            {
                nextFireballTime = Time.time + fireballCooldown;
                LaunchFireball();
            }
            else
            {
                // Permitir que se reanude el movimiento si no está atacando
                anim.SetBool("Attack", false);
            }
        }
    }


    void LaunchFireball()
    {
        // Activar el trigger de ataque
        anim.SetBool("Attack", true);

        // Instanciar la bola de fuego
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();

        // Calcular dirección (basada en el lado hacia el que mira el Wizard)
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Aplicar fuerza
        int x = Random.Range(5, 8);
        int y = Random.Range(5, 8);
        fireballRb.AddForce(new Vector2(fireballRb.mass * x * direction.x, fireballRb.mass * y), ForceMode2D.Impulse);

        // Desactivar animación de ataque tras un pequeño retraso
        Invoke("ResetAttackAnimation", 0.5f); // Asegúrate de que la animación dura menos de 0.5s
    }

    void ResetAttackAnimation()
    {
        anim.SetBool("Attack", false);
    }
    void CheckForWallAndJump()
    {
        // Obtener la posición de los pies del wizard (restamos un pequeño valor en el eje Y)
        Vector2 feetPosition = new Vector2(transform.position.x, transform.position.y - 1.5f);  // Ajusta el valor 0.5f según el tamaño del wizard

        // Raycast para detectar una pared frente al wizard, ahora partiendo desde los pies
        if (isMoving && !isJumping)
        {
            // Raycast hacia la derecha y hacia la izquierda
            RaycastHit2D hitRight = Physics2D.Raycast(feetPosition, Vector2.right, 1f, wallLayer);  // Hacia la derecha
            RaycastHit2D hitLeft = Physics2D.Raycast(feetPosition, Vector2.left, 1f, wallLayer);    // Hacia la izquierda

            // Si detecta alguna pared, hacer que el wizard salte
            if (hitRight.collider != null || hitLeft.collider != null)
            {
                Jump();
            }

            // Visualizar el rayo (opcional, solo para depuración)
            Debug.DrawRay(feetPosition, Vector2.right * 1f, Color.red);  // Rayo hacia la derecha
            Debug.DrawRay(feetPosition, Vector2.left * 1f, Color.red);   // Rayo hacia la izquierda
        }
    }

    void Jump()
    {
        isJumping = true;
        rb.AddForce(new Vector2(0, rb.mass*9f), ForceMode2D.Impulse);   // Aplica fuerza hacia arriba

        // Temporizador para permitir que el wizard deje de estar en estado de salto
        StartCoroutine(ResetJump());
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f); // Espera un poco antes de permitir otro salto
        isJumping = false;
    }
}
