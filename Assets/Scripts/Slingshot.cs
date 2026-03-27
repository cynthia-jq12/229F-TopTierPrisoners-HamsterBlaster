using UnityEngine;
using TMPro;

public class Slingshot : MonoBehaviour
{
    private Rigidbody rb;
    public TextMeshProUGUI forceUI;

    public float chargeSpeed = 800f;
    public float maxForce = 2000f;
    private float currentForce = 0f;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(0, h * 100f * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            isCharging = true;
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0, maxForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            Shoot();
        }

        if (forceUI != null)
        {
            forceUI.text = "Charging Force: " + currentForce.ToString("F0") + " N";
        }
    }

    void Shoot()
    {
        rb.AddForce(transform.forward * currentForce);
        currentForce = 0f;
        isCharging = false;
    }
}