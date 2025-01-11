using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; // Punto de reaparici�n
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath += HandlePlayerDeath; // Suscribirse al evento
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDeath -= HandlePlayerDeath; // Desuscribirse para evitar problemas
        }
    }

    private void HandlePlayerDeath()
    {
        transform.position = respawnPoint.position;
        Debug.Log($"Respawn llamado. Nueva posici�n: {transform.position}");
        playerHealth.Respawn();

    }
}

