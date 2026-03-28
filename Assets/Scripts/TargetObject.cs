using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetObject : MonoBehaviour
{
    public int scoreValue = 10;
    public float destroyDelay = 2f;
    private bool isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHit)
        {
            isHit = true;

            ScoreManager.instance.AddScore(scoreValue);

            Destroy(gameObject, destroyDelay);

            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}