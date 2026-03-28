using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Level 1 Stars")]
    public GameObject[] l1Stars;
    [Header("Level 2 Stars")]
    public GameObject[] l2Stars;
    [Header("Level 3 Stars")]
    public GameObject[] l3Stars;

    void Start()
    {
        UpdateStarDisplay();
    }

    public void UpdateStarDisplay()
    {
        SetStars(1, l1Stars);
        SetStars(2, l2Stars);
        SetStars(3, l3Stars);
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
    public void GoToLevel3() { SceneManager.LoadScene("Level3"); }
    public void GoToCredit() { SceneManager.LoadScene("CreditScene"); }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        UpdateStarDisplay();
    }
}