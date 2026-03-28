using UnityEngine;
using TMPro;
using System.Collections;

public class BoundaryWarning : MonoBehaviour
{
    public float timeAllowedOutside = 5f;
    public GameObject warningPanel;
    public TextMeshProUGUI countdownText;

    private float timeLeft;
    private bool isOutside = false;
    private Coroutine warningCoroutine;

    void Start()
    {
        if (warningPanel != null) warningPanel.SetActive(false);
        timeLeft = timeAllowedOutside;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            isOutside = true;
            if (warningPanel != null) warningPanel.SetActive(true);
            warningCoroutine = StartCoroutine(StartCountdown());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            isOutside = false;
            if (warningPanel != null) warningPanel.SetActive(false);
            if (warningCoroutine != null) StopCoroutine(warningCoroutine);
            timeLeft = timeAllowedOutside;
        }
    }

    IEnumerator StartCountdown()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (countdownText != null)
                countdownText.text = "Return to Area in: " + Mathf.Ceil(timeLeft).ToString() + "s";
            yield return null;
        }
        if (isOutside) GameOverByBoundary();
    }

    void GameOverByBoundary()
    {
        if (TimeManager.instance != null)
            TimeManager.instance.GameOver();
    }
}