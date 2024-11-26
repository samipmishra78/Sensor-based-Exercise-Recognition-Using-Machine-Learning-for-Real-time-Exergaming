using UnityEngine;
using TMPro;
using System;

public class scores: MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;  // Reference to the TextMeshProUGUI component for displaying the score
    [SerializeField]
    private TextMeshProUGUI jumpText;   // Reference to the TextMeshProUGUI component for displaying jump count
    [SerializeField]
    private TextMeshProUGUI slideText;  // Reference to the TextMeshProUGUI component for displaying slide count

    private int score = 0;  // Integer to hold the score
    private int jumpCount = 0;  // Counter for jumps
    private int slideCount = 0;  // Counter for slides
    private Vector3 lastPosition;  // Last position of the player for distance calculation
    private float accumulatedDistance = 0f;  // Accumulated distance traveled by the player
    public float distanceInterval = 1f;  // Interval in distance for score increment
    private bool isStopped = false;  // Flag to stop score updating

    void Start()
    {
        lastPosition = transform.position;  // Initialize last position as the starting position
        UpdateScoreText();  // Update the UI at the start
        UpdateJumpText();   // Initialize jump count display
        UpdateSlideText();  // Initialize slide count display
    }

    void Update()
    {
        // Only update score if not stopped
        if (!isStopped)
        {
            // Calculate the distance traveled since the last frame
            float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
            accumulatedDistance += distanceThisFrame;  // Accumulate distance
            lastPosition = transform.position;  // Update last position

            // Check if accumulated distance has reached the interval
            if (accumulatedDistance >= distanceInterval)
            {
                score += 1;  // Increase score by 1
                accumulatedDistance = 0f;  // Reset accumulated distance
                UpdateScoreText();  // Update the displayed score
            }

            // Detect player input for jump and slide (example keys: space for jump, left shift for slide)
            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpCount += 1;  // Increment jump counter
                UpdateJumpText();  // Update displayed jump count
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                slideCount += 1;  // Increment slide counter
                UpdateSlideText();  // Update displayed slide count
            }
        }
    }

    // Function to update the score text on the UI
    private void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    // Function to update the jump count text on the UI
    private void UpdateJumpText()
    {
        jumpText.text = "J:" + jumpCount.ToString();
    }

    // Function to update the slide count text on the UI
    private void UpdateSlideText()
    {
        slideText.text = "S:" + slideCount.ToString();
    }

    // Optional: Method to add a specific amount to the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Collision detection to stop score updating
    private void OnTriggerEnter(Collider other)
    {
        // Replace "YourObjectTag" with the tag of the object you want to detect
        if (other.CompareTag("obs"))
        {
            isStopped = true;  // Stop score updating on collision
        }
    }
}
