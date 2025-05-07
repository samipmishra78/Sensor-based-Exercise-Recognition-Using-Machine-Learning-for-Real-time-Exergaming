using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WeightInput : MonoBehaviour
{
    public TMP_InputField weightInput;  // Input field reference
    public TextMeshProUGUI errorText;   // Optional error display

    void Start()
    {
        weightInput.Select();
        weightInput.ActivateInputField();
    }

    public void StoreWeightAndStartGame()
    {
        if (!string.IsNullOrEmpty(weightInput.text))
        {
            if (float.TryParse(weightInput.text, out float weight) && weight > 0)
            {
                GameStats.SetPlayerWeight(weight);  // Dynamically store player weight
                Debug.Log("Weight stored: " + GameStats.weightKg);
                SceneManager.LoadScene("game");  // Load Game Scene
            }
            else
            {
                ShowError("Please enter a valid weight!");
            }
        }
        else
        {
            ShowError("Weight field cannot be empty!");
        }
    }

    private void ShowError(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
        }
    }
}
