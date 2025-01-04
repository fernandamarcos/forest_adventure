using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;  // Salud máxima (esto puedes ajustarlo para cada objeto)
    public int currentHealth;  // Salud actual

    void Start()
    {
        currentHealth = maxHealth;  // Inicializa la salud actual con la salud máxima
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Restamos el daño a la salud actual

        if (currentHealth < 0)
            currentHealth = 0;  // Asegurarse de que la salud no baje de 0

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
    private void Die()
    {
        Destroy(gameObject);  // Destruye el objeto cuando muere (puedes personalizar esto)
    }
}
