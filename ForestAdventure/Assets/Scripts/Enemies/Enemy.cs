using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Transform player;
    public float detectionRange = 5f;
    public float attackCooldown = 2f; // Tiempo entre ataques
    public int attackDamage = 5; // Daño del enemigo
    public float nextAttackTime = 0f; // Control del tiempo entre ataques


    // Método para destruir al enemigo
    protected virtual void Die()
    {
        Destroy(gameObject);  // Destruye el GameObject del enemigo
    }

    // Start es un método de Unity que se llama cuando el objeto se inicializa
    protected virtual void Start()
    {
    }

    // Update es un método de Unity que se llama una vez por frame
    protected virtual void Update()
    {
    }

}


