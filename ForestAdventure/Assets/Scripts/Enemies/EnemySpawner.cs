using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // Array de prefabs de enemigos
    [SerializeField] private Transform[] spawnPoints;  // Puntos donde los enemigos pueden aparecer
    [SerializeField] private float spawnInterval = 3f; // Tiempo entre generaciones
    [SerializeField] private int maxEnemies = 1;      // Número máximo de enemigos permitidos
    private int currentEnemyCount = 0;                // Contador de enemigos activos
    private int enemiesKilled = 0;                    // Contador de enemigos muertos

    public delegate void AllEnemiesDefeated();        // Delegado para notificar que todos los enemigos han sido derrotados
    public event AllEnemiesDefeated OnAllEnemiesDefeated;
    private LevelManager levelManager;


    void Start()
    {
        StartCoroutine(SpawnEnemies());
        levelManager = FindObjectOfType<LevelManager>();

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
        int randomIndex = Random.Range(0, Mathf.Min(enemyPrefabs.Length, spawnPoints.Length));
        GameObject selectedEnemy = enemyPrefabs[randomIndex];
        Transform spawnPoint = spawnPoints[randomIndex];

        // Genera el enemigo en el punto de spawn seleccionado
        GameObject spawnedEnemy = Instantiate(selectedEnemy, spawnPoint.position, spawnPoint.rotation);

        // Asigna el spawner al enemigo generado
        EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.SetEnemySpawner(this);
        }

        // Incrementa el contador de enemigos activos
        currentEnemyCount++;
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
