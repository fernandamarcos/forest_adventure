using UnityEngine;

public class AutoAttack : Attack
{
    public float attackRange = 1f; 
    public LayerMask targetLayer; 
    private Animator animator; 
    private bool isAttacking = false; 


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


    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
