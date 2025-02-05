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
        float weightLbs = weightKg * 2.2f; // Convert weight to pounds
        float joggingMinutes = joggingTime / 60f; // Convert seconds to minutes

        // MET-based calorie calculations
        float joggingCalories = ((8.0f * 7.7f * weightLbs) / 200) * (joggingMinutes / 60f);
        float jumpCalories = ((6.0f * 7.7f * weightLbs) / 200) * (jumpCount / 60f);
        float squatCalories = ((3.0f * 7.7f * weightLbs) / 200) * (slideCount / 60f);

        // Total Calories Burned
        caloriesBurned = joggingCalories + jumpCalories + squatCalories;
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
