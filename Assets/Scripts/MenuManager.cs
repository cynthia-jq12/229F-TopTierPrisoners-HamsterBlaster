using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // เพิ่มเพื่อให้เรียกใช้งาน Button ได้

public class MenuManager : MonoBehaviour
{
    public GameObject[] l1Stars;
    public GameObject[] l2Stars;

    public Button creditButton;
    public bool lockCreditUntilComplete = true;

    void Start()
    {
        UpdateStarDisplay();
        CheckCreditUnlock();
    }

    public void UpdateStarDisplay()
    {
        SetStars(1, l1Stars);
        SetStars(2, l2Stars);
    }

    void SetStars(int id, GameObject[] starImages)
    {
        int stars = PlayerPrefs.GetInt("Level_" + id + "_Stars", 0);
        for (int i = 0; i < starImages.Length; i++)
        {
            if (starImages[i] != null)
                starImages[i].SetActive(i < stars);
        }
    }

    void CheckCreditUnlock()
    {
        if (!lockCreditUntilComplete) return;

        int starsL1 = PlayerPrefs.GetInt("Level_1_Stars", 0);
        int starsL2 = PlayerPrefs.GetInt("Level_2_Stars", 0);

        if (starsL1 >= 3 && starsL2 >= 3)
        {
            if (creditButton != null) creditButton.interactable = true;
            Debug.Log("Credits Unlocked!");
        }
        else
        {
            if (creditButton != null) creditButton.interactable = false;
        }
    }

    public void GoToLevel1() { SceneManager.LoadScene("Level1"); }
    public void GoToLevel2() { SceneManager.LoadScene("Level2"); }
    public void GoToCredit() { SceneManager.LoadScene("CreditScene"); }

    public void QuitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        UpdateStarDisplay();
        CheckCreditUnlock();
    }
}