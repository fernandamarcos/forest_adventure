using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyPrefabs; // Los prefabs de los enemigos disponibles

    // Método para crear un enemigo
    public GameObject CreateEnemy(int enemyIndex, Transform spawnPoint)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length)
        {
            Debug.LogError("Index fuera de rango para los prefabs de enemigos.");
            return null;
        }

        // Crear el enemigo en el punto de spawn con la rotación predeterminada
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation);

        return enemy;
    }
}
