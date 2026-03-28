using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject[] l1Stars;
    public GameObject[] l2Stars;

    void Start()
    {
        UpdateStarDisplay();
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
    }
}