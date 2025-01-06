using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float attackCooldown = 2f; // Tiempo entre ataques
    public int attackDamage = 5; // Da�o del enemigo
    protected float nextAttackTime = 0f; // Control del tiempo entre ataques
    protected bool isDamaged;

    // M�todo para destruir al enemigo
    protected virtual void Die()
    {
        Destroy(gameObject);  // Destruye el GameObject del enemigo
    }

    // M�todo para da�ar al jugador solo si no est� atacando
    // M�todo para da�ar al jugador solo si no est� atacando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verificar si el objeto es el jugador
        {
            ManualAttack playerAttack = collision.gameObject.GetComponent<ManualAttack>();
            if (playerAttack != null && playerAttack.IsAttacking() && !isDamaged) // Si el jugador est� atacando y el enemigo no ha sido da�ado
            {
                Health enemyHealth = GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(playerAttack.damage); // Aplicar da�o al enemigo
                    isDamaged = true;  // Marcar que el enemigo ha sido da�ado
                    Debug.Log("Enemigo da�ado por el jugador.");
                }
            }
            else if (!playerAttack.IsAttacking()) // Si el jugador no est� atacando, da�ar al jugador
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null && Time.time >= nextAttackTime)
                {
                    playerHealth.TakeDamage(attackDamage);  // Aplicar da�o al jugador
                    Debug.Log("Player da�ado por el enemigo.");
                    nextAttackTime = Time.time + attackCooldown;  // Reiniciar el cooldown
                }
            }
        }
    }

    // Restablecer flag de da�o despu�s de un corto periodo de tiempo (al final del ciclo de ataque del jugador)
    public void ResetDamageFlag()
    {
        isDamaged = false;
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
