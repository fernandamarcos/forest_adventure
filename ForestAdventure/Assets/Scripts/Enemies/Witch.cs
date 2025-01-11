using System.Collections;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private GameObject player; // Referencia al jugador
    private Transform playerTransform;
    public float hoverHeight = 5f; // Altura a la que vuela el wizard
    public float hoverDistanceX = 3f; // Distancia en X respecto al jugador
    public float moveSpeed = 5f; // Velocidad de movimiento para alcanzar la posición deseada

    public GameObject fireballPrefab; // Prefab de la bola de fuego
    private Transform fireballSpawnPoint; // Punto de lanzamiento de la bola de fuego

    private Animator anim; // Referencia al Animator del wizard

    private float fireballCooldown = 2f; // Intervalo de lanzamiento de bolas de fuego
    private float nextFireballTime = 0f; // Tiempo hasta el siguiente lanzamiento de bola de fuego

    void Start()
    {
        anim = GetComponent<Animator>(); // Obtén el Animator del Wizard
        player = GameObject.FindGameObjectWithTag("Player"); // Encuentra al jugador
        playerTransform = player.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        HoverAroundPlayer(); // Mantente flotando cerca del jugador
        HandleFireballAttack(); // Controla el lanzamiento de bolas de fuego
    }

    void HoverAroundPlayer()
    {
        // Calcula la posición deseada para el wizard
        Vector3 targetPosition = new Vector3(
            playerTransform.position.x + (transform.localScale.x > 0 ? hoverDistanceX : -hoverDistanceX),
            playerTransform.position.y + hoverHeight,
            transform.position.z
        );

        // Interpola suavemente la posición del wizard hacia la posición objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Ajustar dirección del mago mirando hacia el jugador
        if (playerTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f); // Mira a la derecha
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f); // Mira a la izquierda
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
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

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
