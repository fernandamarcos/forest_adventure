using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float fireballSpeed = 10f;  // Velocidad de disparo
    [SerializeField] private float lifetime = 5f;  // Tiempo de vida de la bola de fuego antes de ser destruida
    [SerializeField] private Rigidbody2D rb;  // Rigidbody2D de la bola de fuego
    [SerializeField] private Collider2D col;  // Collider2D de la bola de fuego
    [SerializeField] private PhysicsMaterial2D bounceMaterial; // Material de física para el rebote

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Asignamos el material de rebote
        col.sharedMaterial = bounceMaterial;
        // Destruye la bola de fuego después de un tiempo
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Lógica adicional cuando la bola de fuego colisiona con otros objetos
        // Por ejemplo, podrías dañar a un enemigo
    }

    void Update()
    {
        // Destruir la bola de fuego si se sale de los límites del juego
        if (transform.position.x < -6f || transform.position.x > 45f || transform.position.y < -10f || transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }
}
