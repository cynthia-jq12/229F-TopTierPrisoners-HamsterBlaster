using UnityEngine;
using TMPro;
using System.Collections;

public class BoundaryWarning : MonoBehaviour
{
    public float timeAllowedOutside = 5f;

    public GameObject warningPanel;
    public TextMeshProUGUI countdownText;

    private float timeLeft;
    private bool isInsideZone = true;
    private Coroutine warningCoroutine;

    void Start()
    {
        if (warningPanel != null) warningPanel.SetActive(false);
        timeLeft = timeAllowedOutside;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            Debug.Log("Entered SafeZone: System Secured.");
            isInsideZone = true;
            StopWarning();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            Debug.Log("Exited SafeZone: Starting Countdown...");
            isInsideZone = false;
            StartWarning();
        }
    }

    void StartWarning()
    {
        if (warningPanel != null) warningPanel.SetActive(true);

        if (warningCoroutine != null) StopCoroutine(warningCoroutine);
        warningCoroutine = StartCoroutine(StartCountdown());
    }

    void StopWarning()
    {
        if (warningPanel != null) warningPanel.SetActive(false);

        if (warningCoroutine != null) StopCoroutine(warningCoroutine);
        timeLeft = timeAllowedOutside;
    }

    public void DisableWarning()
    {
        if (warningCoroutine != null) StopCoroutine(warningCoroutine);
        if (warningPanel != null) warningPanel.SetActive(false);
        isInsideZone = true;
    }

    IEnumerator StartCountdown()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (countdownText != null)
            {
                countdownText.text = "<color=red>RETURN TO ZONE!</color>\n" + Mathf.Ceil(timeLeft).ToString() + "s";
            }
            yield return null;
        }

        if (!isInsideZone)
        {
            Debug.Log("Time Up: Player remained outside the zone.");
            if (TimeManager.instance != null)
            {
                TimeManager.instance.GameOver();
            }
        }
    }
}