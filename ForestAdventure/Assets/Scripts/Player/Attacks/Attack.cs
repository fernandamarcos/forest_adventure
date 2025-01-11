using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage = 10; 
    public float attackCooldown = 1f; 
    protected float nextAttackTime = 0f; 
    public abstract void PerformAttack();

    
    protected void ApplyDamage(GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null && CanAttack())
        {
            targetHealth.TakeDamage(damage); 
        }
    }

    
    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

   
    protected void ResetCooldown()
    {
        nextAttackTime = Time.time + attackCooldown;
    }


}
