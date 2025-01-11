using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAttack : Attack
{
    public KeyCode attackKey = KeyCode.Space; 
    public float attackRange = 1f; 
    private bool isAttacking;
    protected bool hasDamaged; 

    [SerializeField] private Animator animator;


    void Start()
    {
        
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
        isAttacking = true;  
        animator.SetBool("isAttacking", true);
        animator.SetTrigger("Attack");
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var target in hitTargets)
        {
            if (target.CompareTag("Enemy"))
            {
                ApplyDamage(target.gameObject);
                ResetCooldown();
            }
        }
        ResetCooldown();

    }
    public void OnAttackAnimationEnd()
    {
        
        isAttacking = false;
        animator.SetBool("isAttacking", false);  
    }
    public bool IsAttacking()
        { return isAttacking; }
}
