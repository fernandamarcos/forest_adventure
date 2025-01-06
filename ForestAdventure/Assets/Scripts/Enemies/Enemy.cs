using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float attackCooldown = 2f; // Tiempo entre ataques
    public int attackDamage = 5; // Daño del enemigo
    protected float nextAttackTime = 0f; // Control del tiempo entre ataques

    // Método para destruir al enemigo
    protected virtual void Die()
    {
        Destroy(gameObject);  // Destruye el GameObject del enemigo
    }

    // Método para dañar al jugador solo si no está atacando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verificar si el objeto es el jugador
        {
            // Verificar si el jugador está atacando
            ManualAttack playerAttack = collision.gameObject.GetComponent<ManualAttack>();
            if (playerAttack != null && !playerAttack.IsAttacking()) // Solo dañar si no está atacando
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null && Time.time >= nextAttackTime) // Aplicar cooldown
                {
                    playerHealth.TakeDamage(attackDamage); // Aplicar daño
                    Debug.Log("Player dañado por el enemigo.");
                    nextAttackTime = Time.time + attackCooldown; // Reiniciar el cooldown
                }
            }
        }
    }


// Start es un método de Unity que se llama cuando el objeto se inicializa
    protected virtual void Start()
    {
    }

    // Update es un método de Unity que se llama una vez por frame
    protected virtual void Update()
    {
    }
}
