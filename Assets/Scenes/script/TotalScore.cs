using UnityEngine;
using TMPro;

public class GameOverScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI totalScoreText; // UI Text for displaying the final score

    private void Start()
    {
        // Display the final score
        if (totalScoreText != null)
        {
            totalScoreText.text = "TOTAL SCORE: " + ScoreManager.Instance.GetCurrentScore().ToString();
        }
    }
}
