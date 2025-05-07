using UnityEngine;

public static class GameStats
{
    public static int jumpCount = 0;
    public static int slideCount = 0;
    public static float joggingTime = 0f;
    public static int finalScore = 0;
    public static float caloriesBurned = 0f;
    public static float weightKg = 70f;

    public static void SetPlayerWeight(float weight)
    {
        weightKg = weight;
    }

    public static void CalculateCaloriesBurned()
    {
        float joggingHours = joggingTime / 3600f;
        float jumpMinutes = jumpCount * 0.3f / 60f;
        float slideMinutes = slideCount * 1.0f / 60f;

        float joggingCalories = 8.0f * weightKg * joggingHours;
        float jumpCalories = 6.0f * weightKg * (jumpMinutes / 60f);
        float squatCalories = 3.0f * weightKg * (slideMinutes / 60f);

        caloriesBurned = (joggingCalories + jumpCalories + squatCalories) * 0.09f;
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
