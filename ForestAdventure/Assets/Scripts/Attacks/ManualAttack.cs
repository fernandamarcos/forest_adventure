using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAttack : Attack
{
    public KeyCode attackKey = KeyCode.Space; // Tecla para atacar
    public float attackRange = 1f; // Rango del ataque
    private bool isAttacking;

    [SerializeField]
    private Animator animator;


    void Start()
    {
        // Obtener el componente Animator del jugador
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on the GameObject!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        isAttacking = true;  // Indica que el ataque está en curso
        animator.SetBool("isAttacking", true);
        animator.SetTrigger("Attack");


        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var target in hitTargets)
        {
            if (target.CompareTag("Enemy"))
            {
                ApplyDamage(target.gameObject);
            }
        }
        ResetCooldown();

        OnAttackAnimationEnd();
        
    }

    public void OnAttackAnimationEnd()
    {
        // Cambiar a otro estado (por ejemplo, "Idle" o "Run")
        isAttacking = false;
        animator.SetBool("isAttacking", false);  // Puedes tener un parámetro "isAttacking" para transitar
    }


    // Visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public bool IsAttacking()
    { return isAttacking; }
}
