using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement; // Necesario para manejar las escenas

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

    private string secondSceneName = "Level2";        // Nombre de la segunda escena (asignar en el Inspector)

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        StartLevel();
        wizardSpawnPoint = wizardPrefab.GetComponentInChildren<Transform>();
    }

    void StartLevel()
    {
        // Si es el nivel 2, cargar la segunda escena
        if (currentLevel == 1) // Segundo nivel (contando desde 0)
        {
            LoadSecondScene();
            return; // Salir del método para evitar que se configure el nivel actual
        }

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

    void LoadSecondScene()
    {
        Debug.Log("Cargando la segunda escena...");
        SceneManager.LoadScene(secondSceneName); // Carga la escena especificada
    }

    public void HandleLevelCompletion()
    {
        currentLevel++;

        if (currentLevel < levelMusic.Length) // If there are more available levels...
        {
            StartLevel(); // Continuar con el siguiente nivel
        }
        else // If there are no more levels (the game finished)...
        {
            Debug.Log("¡Felicidades, has completado todos los niveles!");
            // Final scene or Restart game...
        }
    }
}
