using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI level1StarsText;
    public TextMeshProUGUI level2StarsText;
    public TextMeshProUGUI level3StarsText;

    void Start()
    {
        UpdateStarDisplay();
    }

    public void UpdateStarDisplay()
    {
        if (level1StarsText != null) level1StarsText.text = GetStarString(1);
        if (level2StarsText != null) level2StarsText.text = GetStarString(2);
        if (level3StarsText != null) level3StarsText.text = GetStarString(3);
    }

    string GetStarString(int id)
    {
        int stars = PlayerPrefs.GetInt("Level_" + id + "_Stars", 0);
        string starDisplay = "";
        for (int i = 0; i < stars; i++) starDisplay += "⭐";
        return stars == 0 ? "Locked" : starDisplay;
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        UpdateStarDisplay();
    }
}