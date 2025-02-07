using UnityEngine;

public static class GameStats
{
    public static int jumpCount = 0;
    public static int slideCount = 0;
    public static float joggingTime = 0f; // In seconds
    public static int finalScore = 0;
    public static float caloriesBurned = 0f;

    private const float weightKg = 70f; // Default weight (can be dynamic)

    public static void CalculateCaloriesBurned()
    {
        float joggingHours = joggingTime / 3600f; // Convert seconds to hours
        float jumpMinutes = jumpCount * 0.3f / 60f; // Each jump ~ 0.3 sec
        float slideMinutes = slideCount * 1.0f / 60f; // Each slide ~ 1.0 sec

        // MET-based calorie calculations with reduced values
        float joggingCalories = 6.0f * weightKg * joggingHours;  // Jogging MET = 6.0
        float jumpCalories = 7.0f * weightKg * (jumpMinutes / 60f); // Jumping MET = 7.0
        float squatCalories = 4.0f * weightKg * (slideMinutes / 60f); // Sliding MET = 4.0

        // Apply a slight reduction factor (90%) to further adjust for game pacing
        caloriesBurned = (joggingCalories + jumpCalories + squatCalories) * 0.9f;
    }
     
    public static void ResetStats()
    {
        jumpCount = 0;
        slideCount = 0;
        joggingTime = 0f;
        finalScore = 0;
        caloriesBurned = 0f;
    }
}
