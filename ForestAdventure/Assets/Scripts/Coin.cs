using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1; // Valor de la moneda
    private ScoreManager scoreManager; // Referencia al gestor de puntuaci�n

    void Start()
    {
        // Encuentra el ScoreManager en la escena
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("No se encontr� un ScoreManager en la escena.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprueba si el objeto que entra pertenece a la capa del jugador
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Agrega puntuaci�n y destruye la moneda
            scoreManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
