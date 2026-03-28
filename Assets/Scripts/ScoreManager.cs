using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    [Header("Win System")]
    public GameObject winPanel;
    public string mainMenuName = "MainMenu";

    private int currentScore = 0;
    private int maxScore = 0;
    private bool isLevelComplete = false;

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
        Debug.Log("Max Score for this level: " + maxScore);
    }

    public void AddScore(int points)
    {
        if (isLevelComplete) return;

        currentScore += points;
        UpdateUI();

        if (currentScore >= maxScore && maxScore > 0)
        {
            WinGame();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore} / {maxScore}";
        }
    }

    void WinGame()
    {
        isLevelComplete = true;
        Debug.Log("Level Complete! Full Stars!");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        StartCoroutine(ReturnToMenuRoutine());
    }

    IEnumerator ReturnToMenuRoutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(mainMenuName);
    }
}