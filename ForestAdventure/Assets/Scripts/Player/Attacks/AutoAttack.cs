using UnityEngine;

public class AutoAttack : Attack
{
    public float attackRange = 1f; // Rango de ataque
    public LayerMask targetLayer; // Capa de los objetivos a atacar
    private Animator animator; // Para controlar las animaciones
    private bool isAttacking = false; // Para saber si el personaje está atacando


    void Update()
    {
        if (CanAttack())
        {
            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        Debug.Log($"{gameObject.name} attacked");
        animator.SetTrigger("Attack");
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayer);
        foreach (var target in hitTargets)
        {
            ApplyDamage(target.gameObject);
            Debug.Log($"{gameObject.name} attacked {target.name}!");
        }
        ResetCooldown();
    }


    // Visualizar el rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
