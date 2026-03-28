using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float exitPosition = 1500f;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (transform.localPosition.y > exitPosition)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}