using System.Collections;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private GameObject player; // Referencia al jugador
    private Transform playerTransform;
    public float hoverHeight = 5f; // Altura a la que vuela el wizard
    public float moveSpeed = 5f; // Velocidad de movimiento para alcanzar la posici�n deseada
    public float xThreshold = 3f; // Distancia m�xima permitida en X antes de moverse hacia el jugador
    public float yThreshold = 5f; // Distancia m�xima permitida en Y antes de moverse hacia el jugador

    public GameObject fireballPrefab; // Prefab de la bola de fuego

    private Animator anim; // Referencia al Animator del wizard
    private float fireballCooldown = 2f; // Intervalo de lanzamiento de bolas de fuego
    private float nextFireballTime = 0f; // Tiempo hasta el siguiente lanzamiento de bola de fuego
    private bool isFlipped = false; // Controlar la direcci�n actual del wizard

    void Start()
    {
        anim = GetComponent<Animator>(); // Obt�n el Animator del Wizard
        player = GameObject.FindGameObjectWithTag("Player"); // Encuentra al jugador
        playerTransform = player.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        MoveTowardsPlayer(); // Moverse hacia el jugador si est� fuera del rango
        HandleFireballAttack(); // Controlar el lanzamiento de bolas de fuego
    }

    void MoveTowardsPlayer()
    {
        // Calcular la posici�n deseada
        Vector3 targetPosition = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y + hoverHeight,
            transform.position.z
        );

        // Calcular la diferencia en posici�n
        float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(playerTransform.position.y + hoverHeight - transform.position.y);

        // Si est� fuera de los l�mites en X o Y, moverse hacia la posici�n objetivo
        if (distanceX > xThreshold || distanceY > yThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Ajustar direcci�n solo si cambia significativamente
            if ((playerTransform.position.x > transform.position.x && isFlipped) ||
                (playerTransform.position.x < transform.position.x && !isFlipped))
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        // Invierte la direcci�n
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

        // Calcular direcci�n (basada en el lado hacia el que mira el Wizard)
        Vector2 direction = isFlipped ? Vector2.left : Vector2.right;

        // Aplicar fuerza a la bola de fuego
        int x = Random.Range(5, 8); // Fuerza horizontal
        int y = Random.Range(-1, 1); // Fuerza vertical m�nima
        fireballRb.AddForce(new Vector2(fireballRb.mass * x * direction.x, fireballRb.mass * y), ForceMode2D.Impulse);

        // Desactivar animaci�n de ataque tras un peque�o retraso
        Invoke("ResetAttackAnimation", 0.5f);
    }

    void ResetAttackAnimation()
    {
        anim.SetBool("Attack", false);
    }
}
