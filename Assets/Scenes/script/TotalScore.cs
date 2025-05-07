using UnityEngine;
using TMPro;

public class GameOverScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreDisplayText; // Reference to the TextMeshProUGUI component for displaying the score

    void Start()
    {
        // Access the final score from GameStats, divide it by 1000, and display it
        int finalScoreQuotient = GameStats.finalScore / 1000;  // Divide the final score by 1000 to get the quotient
        scoreDisplayText.text = "FINAL SCORE: " + finalScoreQuotient.ToString();  // Display the quotient value
    }
}
