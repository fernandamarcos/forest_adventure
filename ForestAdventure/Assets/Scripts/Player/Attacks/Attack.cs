using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage = 10; // Da�o infligido por el ataque
    public float attackCooldown = 1f; // Tiempo de espera entre ataques
    protected float nextAttackTime = 0f; // Controla el cooldown del ataque
    public abstract void PerformAttack();

    // M�todo para aplicar da�o a un objeto
    protected void ApplyDamage(GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null && CanAttack())
        {
            targetHealth.TakeDamage(damage); // Aplica da�o si el objetivo tiene salud
        }
    }

    // M�todo com�n para verificar si el ataque est� listo
    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    // Para reiniciar el tiempo del cooldown
    protected void ResetCooldown()
    {
        nextAttackTime = Time.time + attackCooldown;
    }


}
