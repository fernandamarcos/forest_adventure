using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;  // Salud máxima (se puede personalizar para cada objeto)
    private int currentHealth;  // Salud actual
    protected Animator anim;

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud actual con la salud máxima
        anim = GetComponent<Animator>();
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el daño a la salud actual

        if (currentHealth > 0) { // Animación de daño
            anim.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();  // Si la salud llega a 0, llama al método de muerte
        }
    }

    // Método para sanar
    public void Heal(int amount)
    {
        currentHealth += amount;  // Aumentamos la salud

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;  // No permitir que la salud supere el máximo
    }

    // Método para manejar la muerte
    protected virtual void Die()
    {
    }

    public int GetCurrentHealth()
        { return currentHealth; }
}
