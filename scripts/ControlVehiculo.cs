using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class ControlVehiculo : MonoBehaviour
{
    [Header("Movimiento")]
    public float fuerzaAvance = 800f;
    public float fuerzaGiro = 80f;
    public float velocidadMaxima = 15f;

    [Header("Suavidad")]
    public float suavidadGiro = 5f;

    Rigidbody rb;
    float inputVertical;
    float inputHorizontal;
    float giroActual;
    [Header("Velocidad")]
    public float multiplicadorVelocidad = 2f;

    public void ActivarVelocidad(float duracion)
    {
        StartCoroutine(VelocidadCoroutine(duracion));
    }

    IEnumerator VelocidadCoroutine(float duracion)
    {
        velocidadMaxima *= multiplicadorVelocidad;
        fuerzaAvance *= multiplicadorVelocidad;
        yield return new WaitForSeconds(duracion);
        velocidadMaxima /= multiplicadorVelocidad;
        fuerzaAvance /= multiplicadorVelocidad;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");    // GetAxis en vez de GetAxisRaw = suave
        inputHorizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        MoverVehiculo();
        GirarVehiculo();
    }

    void MoverVehiculo()
    {
        if (rb.linearVelocity.magnitude < velocidadMaxima)
        {
            Vector3 fuerza = transform.forward * inputVertical * fuerzaAvance * Time.fixedDeltaTime;
            rb.AddForce(fuerza);
        }
    }

    void GirarVehiculo()
    {
        float velocidadActual = rb.linearVelocity.magnitude;
        if (velocidadActual > 0.5f)
        {
            // Giro interpolado para que sea suave
            giroActual = Mathf.Lerp(giroActual, inputHorizontal * fuerzaGiro, suavidadGiro * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, giroActual * Time.fixedDeltaTime, 0f));
        }
        else
        {
            giroActual = Mathf.Lerp(giroActual, 0f, suavidadGiro * Time.fixedDeltaTime);
        }
    }
}