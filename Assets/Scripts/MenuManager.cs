using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }
}