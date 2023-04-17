using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int listsCompleted;
    private TextMeshProUGUI scoreTextUI; // Reference to the TMP Text for displaying the score

    void Start()
    {
        scoreTextUI = GameObject.Find("Score (TMP)").GetComponent<TextMeshProUGUI>();
        // Initialize the score and update the UI
        score = 0;
        UpdateScoreUI();
    }

    // Call this method to increase the score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // Call this method to decrease the score
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreUI();
    }

    // Call this method to get the current score
    public int GetScore()
    {
        return score;
    }

    // Update the score display in the UI
    void UpdateScoreUI()
    {
        scoreTextUI.text = "Score: " + score;
    }

    public void ListCompleted()
    {
        listsCompleted++;
    }

    public int GetCompletedCount()
    {
        return listsCompleted;
    }
}


