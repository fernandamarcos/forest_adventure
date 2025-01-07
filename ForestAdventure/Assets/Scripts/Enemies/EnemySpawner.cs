using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // Array de prefabs de enemigos
    [SerializeField] private Transform[] spawnPoints; // Puntos donde los enemigos pueden aparecer
    [SerializeField] private float spawnInterval = 3f; // Tiempo entre generaciones
    [SerializeField] private int maxEnemies = 10; // Número máximo de enemigos permitidos
    private int currentEnemyCount = 0; // Contador de enemigos activos

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        // Selecciona un índice aleatorio para el enemigo y el punto de spawn
        int randomIndex = Random.Range(0, Mathf.Min(enemyPrefabs.Length, spawnPoints.Length));

        // Usa el índice aleatorio para seleccionar el enemigo y el punto de spawn
        GameObject selectedEnemy = enemyPrefabs[randomIndex];
        Transform spawnPoint = spawnPoints[randomIndex];

        // Genera el enemigo en el punto de spawn seleccionado
        Instantiate(selectedEnemy, spawnPoint.position, spawnPoint.rotation);

        // Incrementa el contador de enemigos
        currentEnemyCount++;
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
