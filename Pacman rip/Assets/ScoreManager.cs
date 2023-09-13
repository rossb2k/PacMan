using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    public Text gameOverText; 
    public int maxDots = 149; 
    private int score = 0;
    private bool gameOver = false;

    private void Start()
    {
        
        UpdateScoreText();
        gameOverText.text = "";
    }

    // Call this method when a dot is collected
    public void CollectDot()
    {
        if (gameOver) return; // Don't collect dots if the game is over

        score++;

        if (score >= maxDots)
        {
            gameOverText.text = "Game Over";
            
            gameOver = true;
        }

        // Update the UI text
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    
}
