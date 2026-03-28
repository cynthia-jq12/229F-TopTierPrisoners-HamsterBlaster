using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Targets")]
    public Transform target;

    [Header("Settings")]
    public Vector3 offset = new Vector3(0f, 4.55f, -5f);
    public float smoothSpeed = 5f;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        //ตาม Player
        Vector3 basePosition = target.TransformPoint(offset);

        //ขยับตามแบบสวย ๆ
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, basePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        //หมุนกล้อง
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = smoothedRotation;
    }
}