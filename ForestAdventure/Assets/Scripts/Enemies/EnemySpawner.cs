using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;  
    [SerializeField] private float spawnInterval = 3f; 
    [SerializeField] private int maxEnemies = 5;      
    private int currentEnemyCount = 0;                
    private int enemiesKilled = 0;                    
    public delegate void AllEnemiesDefeated();        // Delegado para notificar que todos los enemigos han sido derrotados
    public event AllEnemiesDefeated OnAllEnemiesDefeated;

    [SerializeField] private EnemyFactory enemyFactory;                
    private LevelManager levelManager;

    void Start()
    {
        enemyFactory = FindObjectOfType<EnemyFactory>(); // Encuentra la instancia de EnemyFactory
        levelManager = FindObjectOfType<LevelManager>();

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Selecciona un índice aleatorio para el enemigo y el punto de spawn
        int randomIndex = Random.Range(0, enemyFactory.enemyPrefabs.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Usar la fábrica para crear el enemigo
        GameObject spawnedEnemy = enemyFactory.CreateEnemy(randomIndex, spawnPoint);

        if (spawnedEnemy != null)
        {
            // Asigna el spawner al enemigo generado
            EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.SetEnemySpawner(this);
            }

            // Incrementa el contador de enemigos activos
            currentEnemyCount++;
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;
        enemiesKilled++;

        // Si se han matado suficientes enemigos, invocar el evento de nivel completado
        if (enemiesKilled >= maxEnemies)
        {
            levelManager.HandleLevelCompletion();
        }
    }
}
