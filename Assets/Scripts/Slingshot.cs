using UnityEngine;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private bool isShot = false;

    [Header("UI System")]
    public Text forceUI; // เอาไว้เชื่อมกับ Text ที่เราสร้างใน Step 2

    [Header("Physics: Newton's 2nd Law")]
    public float baseAcceleration = 500f; // ค่าความเร่ง (a)
    public float maxDrag = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void OnMouseDrag()
    {
        if (isShot) return;

        // คำนวณระยะที่ลากเมาส์
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 displacement = startPos - currentPos;
        if (displacement.magnitude > maxDrag)
        {
            displacement = displacement.normalized * maxDrag;
        }
        transform.position = startPos - displacement;

        // โชว์ตัวเลขการคำนวณบนจอ
        float a = displacement.magnitude * baseAcceleration;
        float calculatedForce = rb.mass * a;
        if (forceUI != null) forceUI.text = "Force (F=ma): " + calculatedForce.ToString("F0") + " N";
    }

    void OnMouseUp()
    {
        if (isShot) return;

        Vector3 displacement = startPos - transform.position;

        // --- ส่วนทำคะแนน หัวข้อ C: กฎการเคลื่อนที่ของนิวตัน ---
        float a = displacement.magnitude * baseAcceleration;
        Vector3 direction = displacement.normalized;

        // ใช้สมการ F = ma 
        Vector3 finalForce = direction * (rb.mass * a);

        rb.isKinematic = false;
        rb.AddForce(finalForce);

        isShot = true;
    }
}
