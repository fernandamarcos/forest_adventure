using UnityEngine;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public GameObject enemySpawnerPrefab;  // Enemy Spawner prefab
    public GameObject wizardPrefab;       // Wizard prefab
    private Transform wizardSpawnPoint;    // Wizard spawn point

    public AudioClip[] levelMusic;        // Music for the level
    public AudioSource musicPlayer;       //  AudioSource component which will play the music

    private int currentLevel = 0;         // Level number
    private EnemySpawner currentEnemySpawner; // Reference to current spawner
    private PlayerHealth playerHealth;    // Reference to player's health

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        StartLevel();
        wizardSpawnPoint = wizardPrefab.GetComponentInChildren<Transform>();
    }

    void StartLevel()
    {
        // Check if the currentEnemySpawner already exists
        if (currentEnemySpawner == null)
        {
            // Instantiate a new spawner only if it doesn't exist already
            currentEnemySpawner = Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity).GetComponent<EnemySpawner>();
        }

        // Initialization and configuration of the level (music, enemy spawner, player health..)
        if (levelMusic.Length > currentLevel)
        {
            musicPlayer.clip = levelMusic[currentLevel];
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        // Reset the health and other level-specific settings
        playerHealth.ResetHealth(); // (for next level)

        // Level is completed when all enemies have been killed
        currentEnemySpawner.OnAllEnemiesDefeated += HandleLevelCompletion;
    }

    public void HandleLevelCompletion()
    {
        currentLevel++;

        if (currentLevel < levelMusic.Length) // If there are more available levels...
        {
            if (currentLevel == 1)
            {
                Instantiate(wizardPrefab, wizardSpawnPoint.position, Quaternion.identity);
            }

            // Re-initialize for next level
            StartLevel();
        }
        else // If there are no more levels (the game finished)...
        {
            Debug.Log("¡Felicidades, has completado todos los niveles!");
            // Final scene or Restart game...
        }
    }
}
