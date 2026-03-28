using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    void Awake() { instance = this; }

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        BoundaryWarning boundary = Object.FindFirstObjectByType<BoundaryWarning>();
        if (boundary != null)
        {
            boundary.DisableWarning();
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        if (ScoreManager.instance != null)
            ScoreManager.instance.SetTimeUp();
    }

    public void RetryLevel() { Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void GoToMainMenu() { Time.timeScale = 1f; SceneManager.LoadScene("MainMenu"); }
}