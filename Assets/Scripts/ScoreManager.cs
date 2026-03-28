using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    public GameObject winPanel;
    public string mainMenuName = "MainMenu";
    public int levelID;

    private int currentScore = 0;
    private int maxScore = 0;
    private bool isLevelComplete = false;
    private bool isTimeUp = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        TargetObject[] targets = FindObjectsByType<TargetObject>(FindObjectsSortMode.None);
        foreach (TargetObject target in targets)
        {
            maxScore += target.scoreValue;
        }
        UpdateUI();
    }

    public void AddScore(int points)
    {
        if (isLevelComplete) return;
        currentScore += points;
        UpdateUI();
        if (currentScore >= maxScore && maxScore > 0)
        {
            EndLevel();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore} / {maxScore}";
    }

    public void SetTimeUp()
    {
        isTimeUp = true;
        EndLevel();
    }

    public void EndLevel()
    {
        if (isLevelComplete) return;
        isLevelComplete = true;

        int starsEarned = 0;
        float percentage = (maxScore > 0) ? (float)currentScore / maxScore * 100f : 0;

        if (percentage >= 100) starsEarned = 3;
        else if (percentage >= 70) starsEarned = 2;
        else if (percentage >= 1) starsEarned = 1;

        int previousStars = PlayerPrefs.GetInt("Level_" + levelID + "_Stars", 0);
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt("Level_" + levelID + "_Stars", starsEarned);
            PlayerPrefs.Save();
        }

        if (winPanel != null && !isTimeUp)
            winPanel.SetActive(true);

        StartCoroutine(ReturnToMenuRoutine());
    }

    IEnumerator ReturnToMenuRoutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(mainMenuName);
    }
}