
using UnityEngine;

public class WarriorAttack : MonoBehaviour

{
    public float attackRange = 1f; // Rango del ataque (distancia)
    public int attackDamage = 10;  // Da�o que inflige el ataque
    public float attackCooldown = 0.5f; // Tiempo de espera entre ataques

    private Animator animator; // Para controlar las animaciones
    private float nextAttackTime = 0f; // Para el cooldown entre ataques
    private bool isAttacking = false; // Para saber si el personaje est� atacando

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        // Detectar si el jugador presiona el bot�n de ataque (por ejemplo, el bot�n izquierdo del rat�n)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime && !isAttacking)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Inicia el cooldown
        }
    }

    void Attack()
    {
        // Iniciar la animaci�n de ataque (el trigger activar� la animaci�n)
        isAttacking = true;  // Indica que el ataque est� en curso
        animator.SetTrigger("Attack");

        // Detectar colisiones con los enemigos en el rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // Revisar si alguno de los enemigos es golpeado
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Intentamos obtener el script Enemy (o sus derivados) para hacerle da�o
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                if (enemyScript != null)  // Si el enemigo tiene el script Enemy o derivado
                {
                    enemyScript.TakeDamage(attackDamage);  // Infligir da�o al enemigo
                }
            }
        }
    }

    // Llamada desde la animaci�n al finalizar el ciclo del ataque
    public void OnAttackAnimationEnd()
    {
        // Cambiar a otro estado (por ejemplo, "Idle" o "Run")
        isAttacking = false;
        animator.SetBool("isAttacking", false);  // Puedes tener un par�metro "isAttacking" para transitar
    }

    // Dibujar el rango de ataque en el editor para pruebas
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
