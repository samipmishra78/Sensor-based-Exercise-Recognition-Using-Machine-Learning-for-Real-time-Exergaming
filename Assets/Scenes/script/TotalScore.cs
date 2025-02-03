using UnityEngine;
using TMPro;

public class GameOverScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreDisplayText; // Reference to the TextMeshProUGUI component for displaying the score

    void Start()
    {
        // Retrieve the final score from the scores script and display it
        scoreDisplayText.text = "FINAL SCORE: " + scores.finalScore.ToString();
    }
}