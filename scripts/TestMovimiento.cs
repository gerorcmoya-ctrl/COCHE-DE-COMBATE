using UnityEngine;

public class TestMovimiento : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * 5000f);
        Debug.Log("Velocidad enemigo: " + rb.linearVelocity);
    }
}