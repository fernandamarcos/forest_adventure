using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float attackCooldown = 2f; // Tiempo entre ataques
    public int attackDamage = 5; // Daño del enemigo
    protected float nextAttackTime = 0f; // Control del tiempo entre ataques
    protected bool isDamaged;

    // Método para destruir al enemigo
    protected virtual void Die()
    {
        Destroy(gameObject);  // Destruye el GameObject del enemigo
    }

    // Método para dañar al jugador solo si no está atacando
    // Método para dañar al jugador solo si no está atacando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verificar si el objeto es el jugador
        {
            ManualAttack playerAttack = collision.gameObject.GetComponent<ManualAttack>();
            if (playerAttack != null && playerAttack.IsAttacking() && !isDamaged) // Si el jugador está atacando y el enemigo no ha sido dañado
            {
                Health enemyHealth = GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(playerAttack.damage); // Aplicar daño al enemigo
                    isDamaged = true;  // Marcar que el enemigo ha sido dañado
                    Debug.Log("Enemigo dañado por el jugador.");
                }
            }
            else if (!playerAttack.IsAttacking()) // Si el jugador no está atacando, dañar al jugador
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null && Time.time >= nextAttackTime)
                {
                    playerHealth.TakeDamage(attackDamage);  // Aplicar daño al jugador
                    Debug.Log("Player dañado por el enemigo.");
                    nextAttackTime = Time.time + attackCooldown;  // Reiniciar el cooldown
                }
            }
        }
    }

    // Restablecer flag de daño después de un corto periodo de tiempo (al final del ciclo de ataque del jugador)
    public void ResetDamageFlag()
    {
        isDamaged = false;
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
