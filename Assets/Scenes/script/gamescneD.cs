using UnityEngine;
using TMPro;

public class GameplayScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText; // UI Text for displaying the score

    private void Update()
    {
        // Update the score display during gameplay
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + ScoreManager.Instance.GetCurrentScore().ToString();
        }
    }

    public void AddScore(int amount)
    {
        // Add to the score (can be called by game events)
        ScoreManager.Instance.AddScore(amount);
    }
}
