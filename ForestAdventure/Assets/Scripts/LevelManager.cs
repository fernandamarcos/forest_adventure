using UnityEngine;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public GameObject enemySpawnerPrefab;  // Prefab del Spawner de Enemigos
    public GameObject wizardPrefab;       // Prefab del Wizard (Maligno)
    private Transform wizardSpawnPoint;    // Punto de spawn del Wizard (Maligno)

    public AudioClip[] levelMusic;        // M�sica para cada nivel
    public AudioSource musicPlayer;      // Componente AudioSource que reproduce la m�sica

    private int currentLevel = 0;        // N�mero del nivel actual
    private EnemySpawner currentEnemySpawner; // Referencia al spawner actual
    private PlayerHealth playerHealth;   // Referencia a la salud del jugador

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // Buscar el script de salud del jugador
        StartLevel(); // Iniciar el primer nivel
        wizardSpawnPoint = wizardPrefab.GetComponentInChildren<Transform>();
    }

    void StartLevel()
    {
        // Cambiar la m�sica seg�n el nivel actual
        if (levelMusic.Length > currentLevel)
        {
            musicPlayer.clip = levelMusic[currentLevel];
            musicPlayer.loop = true; // Repetir la m�sica
            musicPlayer.Play();
        }

        // Si hay un spawner de enemigos activo de niveles anteriores, destruirlo
        if (currentEnemySpawner != null)
        {
            Destroy(currentEnemySpawner.gameObject);
        }

        // Instanciar el Spawner de Enemigos para este nivel
        currentEnemySpawner = Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity).GetComponent<EnemySpawner>();

        // Configurar el evento para cuando todos los enemigos sean derrotados
        currentEnemySpawner.OnAllEnemiesDefeated += HandleLevelCompletion;

        // Reiniciar la vida del jugador al comienzo de cada nivel
        playerHealth.ResetHealth();
    }

    public void HandleLevelCompletion()
    {
        // Cambiar al siguiente nivel
        currentLevel++;

        // Si hay m�s niveles disponibles
        if (currentLevel < levelMusic.Length)
        {
            // En el segundo nivel, instanciar al Wizard
            if (currentLevel == 1) // El segundo nivel tiene el �ndice 1
            {
                Instantiate(wizardPrefab, wizardSpawnPoint.position, Quaternion.identity);
            }

            // Iniciar el siguiente nivel
            StartLevel();
        }
        else
        {
            // Si no hay m�s niveles, finalizar el juego
            Debug.Log("�Felicidades, has completado todos los niveles!");
            // Aqu� puedes cargar una escena final o reiniciar el juego
        }
    }
}
