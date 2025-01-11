using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement; 

public class LevelManager : MonoBehaviour
{
    public GameObject enemySpawnerPrefab;  

    public AudioClip[] levelMusic;        
    public AudioSource musicPlayer;       

    private int currentLevel = 0;         
    private EnemySpawner currentEnemySpawner; // Reference to current spawner
    private PlayerHealth playerHealth;    

    private string secondSceneName = "Level2";        

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        StartLevel();
        
    }

    void StartLevel()
    {
        // If on level 1 go to level 2
        if (currentLevel == 1) 
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

        
        if (levelMusic.Length > currentLevel)
        {
            musicPlayer.clip = levelMusic[currentLevel];
            musicPlayer.loop = true;
            musicPlayer.Play();
        }

        
        playerHealth.ResetHealth(); // (for next level)

        
        currentEnemySpawner.OnAllEnemiesDefeated += HandleLevelCompletion;
    }

    void LoadSecondScene()
    {
        Debug.Log("Cargando la segunda escena...");
        SceneManager.LoadScene(secondSceneName); 
    }

    public void HandleLevelCompletion()
    {
        currentLevel++;

        if (currentLevel < levelMusic.Length) 
        {
            StartLevel(); 
        }
        else 
        {
            Debug.Log("¡Felicidades, has completado todos los niveles!");
            
        }
    }
}
