using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI (Slider)

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;  // Salud máxima
    protected int currentHealth;  // Salud actual
    protected Animator anim;

    [SerializeField] private Slider healthBar;  // Barra de vida (Slider)

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;  // Inicializa la salud con el valor máximo

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;  // Establece el valor máximo de la barra
            healthBar.value = currentHealth;  // Establece el valor inicial de la barra
        }
    }

    // Método para recibir daño
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el daño a la salud actual

        if (currentHealth < 0)
            currentHealth = 0;  // No permitir que la salud sea negativa

        if (healthBar != null)
        {
            healthBar.value = currentHealth;  // Actualiza el valor de la barra de vida
        }

        if (currentHealth <= 0)
        {
            Die();  // Si la salud llega a 0 o menos, llama al método de muerte
        }
    }

    // Método para sanar
    public void Heal(int amount)
    {
        currentHealth += amount;  // Aumentamos la salud

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;  // No permitir que la salud supere el máximo

        if (healthBar != null)
        {
            healthBar.value = currentHealth;  // Actualiza el valor de la barra de vida
        }
    }

    // Método para manejar la muerte
    protected virtual void Die()
    {
        // Aquí puedes añadir lógica común para la muerte (animaciones, efectos, etc.)
        Debug.Log($"{gameObject.name} ha muerto");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    protected void SetCurrentHealth(int value)
        { currentHealth = value; }
}
