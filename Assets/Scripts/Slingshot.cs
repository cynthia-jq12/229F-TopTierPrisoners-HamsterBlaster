using UnityEngine;
using TMPro;

public class Slingshot : MonoBehaviour
{
    private Rigidbody rb;
    private LineRenderer line;
    public TextMeshProUGUI forceUI;

    public float chargeSpeed = 1000f;
    public float maxForce = 3000f;
    private float currentForce = 0f;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(0, h * 150f * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            isCharging = true;
            line.enabled = true;
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0, maxForce);

            DrawAimLine();
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            Shoot();
        }

        if (forceUI != null)
        {
            float acceleration = currentForce / rb.mass;

            forceUI.text = "Acceleration: " + acceleration.ToString("F1") + " m/s2 " + "Mass: " + rb.mass + " kg " + "Force: " + currentForce.ToString("F0") + " N";
        }
    }

    void DrawAimLine()
    {
        line.SetPosition(0, transform.position);
        Vector3 endPoint = transform.position + (transform.forward * (currentForce / 100f));
        line.SetPosition(1, endPoint);
    }

    void Shoot()
    {
        line.enabled = false;
        rb.AddForce(transform.forward * currentForce);
        currentForce = 0f;
        isCharging = false;
    }
}