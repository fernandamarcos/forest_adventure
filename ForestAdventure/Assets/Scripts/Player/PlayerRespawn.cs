using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; // Punto de reaparición
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
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            transform.position = respawnPoint.position;
            Debug.Log($"Respawn llamado. Nueva posición: {transform.position}");
            playerHealth.Respawn();
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}

