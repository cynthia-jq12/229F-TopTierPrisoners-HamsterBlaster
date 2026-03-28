using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 4.55f, -5f);
    public float smoothSpeed = 5f;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 basePosition = target.TransformPoint(offset);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, basePosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        Quaternion baseRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, baseRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = smoothRotation;
    }
}