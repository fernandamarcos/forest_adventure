using UnityEngine;

public class Fireball : Enemy
{
    [SerializeField] private float fireballSpeed = 10f;  // Velocidad de disparo
    [SerializeField] private float lifetime = 5f;  // Tiempo de vida de la bola de fuego antes de ser destruida
    [SerializeField] private Rigidbody2D rb;  // Rigidbody2D de la bola de fuego
    [SerializeField] private Collider2D col;  // Collider2D de la bola de fuego
    [SerializeField] private PhysicsMaterial2D bounceMaterial; // Material de física para el rebote

    protected override void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Asignamos el material de rebote
        col.sharedMaterial = bounceMaterial;
        // Destruye la bola de fuego después de un tiempo
        Destroy(gameObject, lifetime);
    }


    protected override void Update()
    {
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player")) // Verificar si el objeto es el jugador
        {
            Die();
        }
    }
}
