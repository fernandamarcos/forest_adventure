using UnityEngine;
using UnityEngine.UI;  

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;  
    protected int currentHealth;  
    protected Animator anim;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;  

    }

    // Método para recibir daño
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;  

        if (currentHealth < 0)
            currentHealth = 0;  


        if (currentHealth <= 0)
        {
            Die();  
        }
    }

    // Método para sanar
    public void Heal(int amount)
    {
        currentHealth += amount;  

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;  

    }

    
    protected virtual void Die()
    {
        
        Debug.Log($"{gameObject.name} ha muerto");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    protected void SetCurrentHealth(int value)
        { currentHealth = value; }

    public void ResetHealth()
        { currentHealth = maxHealth; }

}
