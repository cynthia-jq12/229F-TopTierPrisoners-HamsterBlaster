using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private bool isShot = false;

    public TMPro.TextMeshProUGUI forceUI;

    public float baseAcceleration = 500f;
    public float maxDrag = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void OnMouseDrag()
    {
        if (isShot) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 displacement = startPos - currentPos;
        if (displacement.magnitude > maxDrag)
        {
            displacement = displacement.normalized * maxDrag;
        }
        transform.position = startPos - displacement;

        float a = displacement.magnitude * baseAcceleration;
        float calculatedForce = rb.mass * a;
        if (forceUI != null) forceUI.text = "Force (F=ma): " + calculatedForce.ToString("F0") + " N";
    }

    void OnMouseUp()
    {
        if (isShot) return;

        Vector3 displacement = startPos - transform.position;

        float a = displacement.magnitude * baseAcceleration;
        Vector3 direction = displacement.normalized;

        Vector3 finalForce = direction * (rb.mass * a);

        rb.isKinematic = false;
        rb.AddForce(finalForce);

        isShot = true;
    }
}