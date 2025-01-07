using System.Collections;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private GameObject player; // El jugador
    private Transform playerTransform;
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float stopDistance = 10f; // Distancia para detenerse
    public GameObject fireballPrefab; // Prefab de la bola de fuego
    private Transform fireballSpawnPoint; // Punto de lanzamiento de la bola de fuego

    public float jumpForce = 10f; // Fuerza del salto
    public LayerMask wallLayer; // Capa de las paredes para el salto

    private Animator anim; // Referencia al Animator del wizard
    private bool isMoving = false; // Flag para saber si el wizard está moviéndose
    private bool isJumping = false; // Flag para saber si está saltando

    private float fireballCooldown = 3f; // Intervalo de lanzamiento de bolas de fuego
    private float nextFireballTime = 0f; // Tiempo hasta el siguiente lanzamiento de bola de fuego

    private Vector3 lastPosition; // Posición previa del wizard
    private float timeSinceLastMove = 0f; // Tiempo desde el último movimiento

    void Start()
    {
        anim = GetComponent<Animator>(); // Obtén el Animator del Wizard
        lastPosition = transform.position; // Inicializa la posición inicial del wizard
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        MoveTowardsPlayer();
        CheckForWallAndJump();
        CheckForIdleWhileWalking();
    }

    void MoveTowardsPlayer()
    {
        // Calcula la distancia entre el wizard y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stopDistance)
        {
            isMoving = true;

            // Mover hacia el jugador directamente usando transform.position
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = newPosition;

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
        }
    }

    void LaunchFireball()
    {
        // Activar el trigger de ataque
        anim.SetBool("Attack", true);

        // Instanciar la bola de fuego
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();

        // Calcular dirección (basada en el lado hacia el que mira el Wizard)
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Aplicar fuerza
        int x = Random.Range(5, 8);
        int y = Random.Range(5, 8);
        fireballRb.AddForce(new Vector2(fireballRb.mass * x * direction.x, fireballRb.mass * y), ForceMode2D.Impulse);



        // Desactivar animación de ataque tras un pequeño retraso
        Invoke("ResetAttackAnimation", 0.5f);
    }

    void ResetAttackAnimation()
    {
        anim.SetBool("Attack", false);
    }

    void CheckForWallAndJump()
    {
        if (isMoving && !isJumping)
        {
            // Raycast hacia adelante para detectar una pared
            Vector2 rayDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1f, wallLayer);

            if (hit.collider != null)
            {
                Jump();
            }

            // Visualizar el rayo (opcional, solo para depuración)
            Debug.DrawRay(transform.position, rayDirection * 1f, Color.red);
        }
    }

    void Jump()
    {
        isJumping = true;

        // Elevar al mago manualmente en el eje Y
        transform.position += new Vector3(0, 2f, 0);

        // Temporizador para permitir que el wizard deje de estar en estado de salto
        StartCoroutine(ResetJump());
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f); // Espera un poco antes de permitir otro salto
        isJumping = false;
    }

    void CheckForIdleWhileWalking()
    {
        if (isMoving && !isJumping)
        {
            // Verificar si la posición ha cambiado
            if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
            {
                timeSinceLastMove += Time.deltaTime;

                // Si han pasado más de 2 segundos sin moverse, salta
                if (timeSinceLastMove >= 1f)
                {
                    Jump();
                    timeSinceLastMove = 0f; // Reiniciar temporizador
                }
            }
            else
            {
                // Si el wizard se ha movido, reinicia el temporizador y actualiza la posición
                timeSinceLastMove = 0f;
                lastPosition = transform.position;
            }
        }
    }
}
