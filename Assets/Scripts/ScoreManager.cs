using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private int maxScore = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TargetObject[] targets = FindObjectsByType<TargetObject>(FindObjectsSortMode.None);

        foreach (TargetObject target in targets)
        {
            maxScore += target.scoreValue;
        }

        UpdateUI();
        Debug.Log("Max Score for this level: " + maxScore);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateUI();

        if (currentScore >= maxScore)
        {
            Debug.Log("Level Complete! Full Stars!");
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore} / {maxScore}";
        }
    }
}