
using UnityEngine;

public class WarriorAttack : MonoBehaviour

{
    public float attackRange = 1f; // Rango del ataque (distancia)
    public int attackDamage = 10;  // Daño que inflige el ataque
    public float attackCooldown = 0.5f; // Tiempo de espera entre ataques

    private Animator animator; // Para controlar las animaciones
    private float nextAttackTime = 0f; // Para el cooldown entre ataques
    private bool isAttacking = false; // Para saber si el personaje está atacando

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        // Detectar si el jugador presiona el botón de ataque (por ejemplo, el botón izquierdo del ratón)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime && !isAttacking)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Inicia el cooldown
        }
    }

    void Attack()
    {
        // Iniciar la animación de ataque (el trigger activará la animación)
        isAttacking = true;  // Indica que el ataque está en curso
        animator.SetTrigger("Attack");

        // Detectar colisiones con los enemigos en el rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // Revisar si alguno de los enemigos es golpeado
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Intentamos obtener el script Enemy (o sus derivados) para hacerle daño
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                if (enemyScript != null)  // Si el enemigo tiene el script Enemy o derivado
                {
                    enemyScript.TakeDamage(attackDamage);  // Infligir daño al enemigo
                }
            }
        }
    }

    // Llamada desde la animación al finalizar el ciclo del ataque
    public void OnAttackAnimationEnd()
    {
        // Cambiar a otro estado (por ejemplo, "Idle" o "Run")
        isAttacking = false;
        animator.SetBool("isAttacking", false);  // Puedes tener un parámetro "isAttacking" para transitar
    }

    // Dibujar el rango de ataque en el editor para pruebas
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
