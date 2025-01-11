using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float attackCooldown = 2f; 
    public int attackDamage = 5; 
    protected float nextAttackTime = 0f; 
    protected bool isDamaged;

    
    protected virtual void Die()
    {
        Destroy(gameObject);  
    }

    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            ManualAttack playerAttack = collision.gameObject.GetComponent<ManualAttack>();
            if (playerAttack != null && playerAttack.IsAttacking() && !isDamaged) 
            {
                Health enemyHealth = GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(playerAttack.damage); 
                    isDamaged = true;  
                    Debug.Log("Enemigo dañado por el jugador.");
                }
            }
            else if (!playerAttack.IsAttacking()) // Si el jugador no está atacando, dañar al jugador
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null && Time.time >= nextAttackTime)
                {
                    playerHealth.TakeDamage(attackDamage);  
                    Debug.Log("Player dañado por el enemigo.");
                    nextAttackTime = Time.time + attackCooldown;  
                }
            }
        }
    }

    // Restablecer flag de daño después de un corto periodo de tiempo (al final del ciclo de ataque del jugador)
    public void ResetDamageFlag()
    {
        isDamaged = false;
    }

    public void Start() {}
    public void Update() {}
}
