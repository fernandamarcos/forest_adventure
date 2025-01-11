using System.Collections;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private GameObject player; // Referencia al jugador
    private Transform playerTransform;
    public float hoverHeight = 5f; // Altura a la que vuela el wizard
    public float moveSpeed = 5f; // Velocidad de movimiento para alcanzar la posición deseada
    public float xThreshold = 3f; // Distancia máxima permitida en X antes de moverse hacia el jugador
    public float yThreshold = 5f; // Distancia máxima permitida en Y antes de moverse hacia el jugador

    public GameObject fireballPrefab; // Prefab de la bola de fuego

    private Animator anim; // Referencia al Animator del wizard
    private float fireballCooldown = 2f; // Intervalo de lanzamiento de bolas de fuego
    private float nextFireballTime = 0f; // Tiempo hasta el siguiente lanzamiento de bola de fuego
    private bool isFlipped = false; // Controlar la dirección actual del wizard

    void Start()
    {
        anim = GetComponent<Animator>(); // Obtén el Animator del Wizard
        player = GameObject.FindGameObjectWithTag("Player"); // Encuentra al jugador
        playerTransform = player.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        MoveTowardsPlayer(); // Moverse hacia el jugador si está fuera del rango
        HandleFireballAttack(); // Controlar el lanzamiento de bolas de fuego
    }

    void MoveTowardsPlayer()
    {
        // Calcular la posición deseada
        Vector3 targetPosition = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y + hoverHeight,
            transform.position.z
        );

        // Calcular la diferencia en posición
        float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(playerTransform.position.y + hoverHeight - transform.position.y);

        // Si está fuera de los límites en X o Y, moverse hacia la posición objetivo
        if (distanceX > xThreshold || distanceY > yThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Ajustar dirección solo si cambia significativamente
            if ((playerTransform.position.x > transform.position.x && isFlipped) ||
                (playerTransform.position.x < transform.position.x && !isFlipped))
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        // Invierte la dirección
        isFlipped = !isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void HandleFireballAttack()
    {
        // Si ha pasado suficiente tiempo, lanza una bola de fuego
        if (Time.time > nextFireballTime)
        {
            nextFireballTime = Time.time + fireballCooldown;
            LaunchFireball();
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
        Vector2 direction = isFlipped ? Vector2.left : Vector2.right;

        // Aplicar fuerza a la bola de fuego
        int x = Random.Range(5, 8); // Fuerza horizontal
        int y = Random.Range(-1, 1); // Fuerza vertical mínima
        fireballRb.AddForce(new Vector2(fireballRb.mass * x * direction.x, fireballRb.mass * y), ForceMode2D.Impulse);

        // Desactivar animación de ataque tras un pequeño retraso
        Invoke("ResetAttackAnimation", 0.5f);
    }

    void ResetAttackAnimation()
    {
        anim.SetBool("Attack", false);
    }
}
