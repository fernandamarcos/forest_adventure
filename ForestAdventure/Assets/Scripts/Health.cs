using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;  // Salud m�xima (se puede personalizar para cada objeto)
    private int currentHealth;  // Salud actual
    protected Animator anim;

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud actual con la salud m�xima
        anim = GetComponent<Animator>();
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el da�o a la salud actual

        if (currentHealth > 0) { // Animaci�n de da�o
            anim.SetTrigger("Hurt");
        }

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
}
