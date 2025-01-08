using UnityEngine;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public GameObject enemySpawnerPrefab;  // Enemy Spawner prefab
    public GameObject wizardPrefab;       // Wizard prefab
    private Transform wizardSpawnPoint;    // Wizard spawn point

    public AudioClip[] levelMusic;        // Music for the level
    public AudioSource musicPlayer;      //  AudioSource component which will play the music

    private int currentLevel = 0;        // Level number
    private EnemySpawner currentEnemySpawner; // Reference to current spawner
    private PlayerHealth playerHealth;   // Reference to player's health

    void Start()
    {

        playerHealth = FindObjectOfType<PlayerHealth>(); 
        StartLevel(); 
        wizardSpawnPoint = wizardPrefab.GetComponentInChildren<Transform>();
    }

    void StartLevel()
    {

        // Initialization and configuration of the level (music, enemy spawner, player health..)

        if (levelMusic.Length > currentLevel)
        {
            musicPlayer.clip = levelMusic[currentLevel];
            musicPlayer.loop = true; 
            musicPlayer.Play();
        }

        if (currentEnemySpawner != null)
        {
            Destroy(currentEnemySpawner.gameObject);
        }

        currentEnemySpawner = Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity).GetComponent<EnemySpawner>();

        // Level is completed when all enemies have been killed
        currentEnemySpawner.OnAllEnemiesDefeated += HandleLevelCompletion;

        playerHealth.ResetHealth(); // (for next level)
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

            
            StartLevel();
        }
        else // If there are no more levels (the game finished)...
        {
            
            Debug.Log("¡Felicidades, has completado todos los niveles!");
            // Final scene or Restart game...
        }
    }
}
