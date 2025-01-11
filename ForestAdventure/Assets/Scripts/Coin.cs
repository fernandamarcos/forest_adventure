using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1; 
    private ScoreManager scoreManager; 

    void Start()
    {
        
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("No se encontró un ScoreManager en la escena.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Chechs if object is the player
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            scoreManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
