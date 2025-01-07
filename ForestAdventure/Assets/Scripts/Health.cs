using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI (Slider)

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;  // Salud m�xima
    protected int currentHealth;  // Salud actual
    protected Animator anim;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;  // Inicializa la salud con el valor m�ximo

    }

    // M�todo para recibir da�o
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el da�o a la salud actual

        if (currentHealth < 0)
            currentHealth = 0;  // No permitir que la salud sea negativa


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
