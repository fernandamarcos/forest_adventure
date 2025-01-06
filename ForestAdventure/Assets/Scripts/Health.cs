using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI (Slider)

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;  // Salud m�xima
    protected int currentHealth;  // Salud actual
    protected Animator anim;

    [SerializeField] private Slider healthBar;  // Barra de vida (Slider)

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;  // Inicializa la salud con el valor m�ximo

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;  // Establece el valor m�ximo de la barra
            healthBar.value = currentHealth;  // Establece el valor inicial de la barra
        }
    }

    // M�todo para recibir da�o
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el da�o a la salud actual

        if (currentHealth < 0)
            currentHealth = 0;  // No permitir que la salud sea negativa

        if (healthBar != null)
        {
            healthBar.value = currentHealth;  // Actualiza el valor de la barra de vida
        }

        if (currentHealth <= 0)
        {
            Die();  // Si la salud llega a 0 o menos, llama al m�todo de muerte
        }
    }

    // M�todo para sanar
    public void Heal(int amount)
    {
        currentHealth += amount;  // Aumentamos la salud

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;  // No permitir que la salud supere el m�ximo

        if (healthBar != null)
        {
            healthBar.value = currentHealth;  // Actualiza el valor de la barra de vida
        }
    }

    // M�todo para manejar la muerte
    protected virtual void Die()
    {
        // Aqu� puedes a�adir l�gica com�n para la muerte (animaciones, efectos, etc.)
        Debug.Log($"{gameObject.name} ha muerto");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    protected void SetCurrentHealth(int value)
        { currentHealth = value; }
}
