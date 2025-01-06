using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;  // Salud m�xima (se puede personalizar para cada objeto)
    protected int currentHealth;  // Salud actual
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    // M�todo para recibir da�o
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el da�o a la salud actual


        if (currentHealth <= 0)
        {
            Die();  // Si la salud llega a 0, llama al m�todo de muerte
        }
    }

    // M�todo para sanar
    public void Heal(int amount)
    {
        currentHealth += amount;  // Aumentamos la salud

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;  // No permitir que la salud supere el m�ximo
    }

    // M�todo para manejar la muerte
    protected virtual void Die()
    {
    }

    public int GetCurrentHealth()
        { return currentHealth; }

    protected void SetCurrentHealth(int health) 
    {  currentHealth = health; }
}
