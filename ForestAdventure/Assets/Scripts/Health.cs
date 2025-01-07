using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI (Slider)

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;  // Salud máxima
    protected int currentHealth;  // Salud actual
    protected Animator anim;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;  // Inicializa la salud con el valor máximo

    }

    // Método para recibir daño
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el daño a la salud actual

        if (currentHealth < 0)
            currentHealth = 0;  // No permitir que la salud sea negativa


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
