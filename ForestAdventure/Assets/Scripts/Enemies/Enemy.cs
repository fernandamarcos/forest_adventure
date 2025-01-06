using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float attackCooldown = 2f; // Tiempo entre ataques
    public int attackDamage = 5; // Da�o del enemigo
    protected float nextAttackTime = 0f; // Control del tiempo entre ataques

    // M�todo para destruir al enemigo
    protected virtual void Die()
    {
        Destroy(gameObject);  // Destruye el GameObject del enemigo
    }

    // M�todo para da�ar al jugador solo si no est� atacando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verificar si el objeto es el jugador
        {
            // Verificar si el jugador est� atacando
            ManualAttack playerAttack = collision.gameObject.GetComponent<ManualAttack>();
            if (playerAttack != null && !playerAttack.IsAttacking()) // Solo da�ar si no est� atacando
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null && Time.time >= nextAttackTime) // Aplicar cooldown
                {
                    playerHealth.TakeDamage(attackDamage); // Aplicar da�o
                    Debug.Log("Player da�ado por el enemigo.");
                    nextAttackTime = Time.time + attackCooldown; // Reiniciar el cooldown
                }
            }
        }
    }


// Start es un m�todo de Unity que se llama cuando el objeto se inicializa
    protected virtual void Start()
    {
    }

    // Update es un m�todo de Unity que se llama una vez por frame
    protected virtual void Update()
    {
    }
}
