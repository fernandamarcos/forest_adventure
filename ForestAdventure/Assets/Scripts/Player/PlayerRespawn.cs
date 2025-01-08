using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; // Donde vuelves a aparecer
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();

    }

    public void Respawn()
    {
        
        transform.position = respawnPoint.position; // Takes player back to starting point
        Debug.Log($"Respawn llamado. Nueva posición: {transform.position}");
        playerHealth.Respawn();
    }
}
