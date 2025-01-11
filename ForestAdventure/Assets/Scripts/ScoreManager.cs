using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    private int currentScore = 0; 

    void Start()
    {
        UpdateScoreUI(); 
    }

    public void AddScore(int amount)
    {
        currentScore += amount; 
        UpdateScoreUI(); 

        // Condition to win - all coins have been collected
        if (currentScore >= 22)
        {
            LoadYouWinScene();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore; 
    }

    void LoadYouWinScene()
    {
        
        SceneManager.LoadScene("YouWin"); 
    }
}
