using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Referencia al texto UI que muestra la puntuación
    private int currentScore = 0; // Puntuación actual

    void Start()
    {
        UpdateScoreUI(); // Inicializa el texto de la puntuación
    }

    public void AddScore(int amount)
    {
        currentScore += amount; // Sumar puntos
        UpdateScoreUI(); // Actualizar la UI

        // Comprobar si la puntuación ha alcanzado 22 y cambiar a la escena "YouWin"
        if (currentScore >= 22)
        {
            LoadYouWinScene();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore; // Actualiza el texto en la interfaz
    }

    void LoadYouWinScene()
    {
        // Cargar la escena "YouWin"
        SceneManager.LoadScene("YouWin"); // Asegúrate de que esta escena esté en las Build Settings
    }
}
