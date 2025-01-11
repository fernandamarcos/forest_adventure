using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyPrefabs; 

    
    public GameObject CreateEnemy(int enemyIndex, Transform spawnPoint)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length)
        {
            Debug.LogError("Index fuera de rango para los prefabs de enemigos.");
            return null;
        }

        
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation);

        return enemy;
    }
}
