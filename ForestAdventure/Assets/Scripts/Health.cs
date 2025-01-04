using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;  // Salud m�xima (esto puedes ajustarlo para cada objeto)
    public int currentHealth;  // Salud actual

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud actual con la salud m�xima
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el da�o a la salud actual

        if (currentHealth < 0)
            currentHealth = 0;  // Asegurarse de que la salud no baje de 0

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
    private void Die()
    {
        Destroy(gameObject);  // Destruye el objeto cuando muere (puedes personalizar esto)
    }
}
