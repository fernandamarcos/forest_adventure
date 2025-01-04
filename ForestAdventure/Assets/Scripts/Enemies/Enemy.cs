using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;  // Salud com�n a todos los enemigos

    // M�todo para recibir da�o

    protected void SetHealth(int newHealth)
    {
        health = newHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;  // Reducir la salud
        if (health <= 0)
        {
            Die();  // Si la salud es 0 o menos, el enemigo muere
        }
    }

    // M�todo para destruir al enemigo
    protected virtual void Die()
    {
        Destroy(gameObject);  // Destruye el GameObject del enemigo
    }

    // Start es un m�todo de Unity que se llama cuando el objeto se inicializa
    protected virtual void Start()
    {
    }

    // Update es un m�todo de Unity que se llama una vez por frame
    protected virtual void Update()
    {
    }
}

