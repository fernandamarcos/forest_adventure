using UnityEngine;
using UnityEngine.UI;

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
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore; // Actualiza el texto en la interfaz
    }
}
