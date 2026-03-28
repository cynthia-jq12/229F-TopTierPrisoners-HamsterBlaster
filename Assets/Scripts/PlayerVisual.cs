using UnityEngine;

public class BallRoller : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float rollSpeed = 100f;

    void Update()
    {
        if (playerRigidbody == null) return;

        transform.localPosition = Vector3.zero;

        Vector3 velocity = playerRigidbody.linearVelocity;

        if (velocity.magnitude > 0.1f)
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, velocity).normalized;

            transform.Rotate(rotationAxis, velocity.magnitude * rollSpeed * Time.deltaTime, Space.World);
        }
    }
}