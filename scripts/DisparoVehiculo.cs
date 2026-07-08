using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DisparoVehiculo : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 30f;
    public float cadencia = 0.3f;

    [Header("Doble Disparo")]
    public Transform puntoDisparoIzq;
    public Transform puntoDisparoDer;
    bool dobleDisparoActivo = false;

    float tiempoUltimoDisparo;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && PuedoDisparar())
        {
            Disparar();
        }
    }

    bool PuedoDisparar()
    {
        return Time.time >= tiempoUltimoDisparo + cadencia;
    }

    void Disparar()
    {
        tiempoUltimoDisparo = Time.time;

        if (dobleDisparoActivo)
        {
            SpawnBala(puntoDisparoIzq);
            SpawnBala(puntoDisparoDer);
        }
        else
        {
            SpawnBala(puntoDisparo);
        }
    }

    void SpawnBala(Transform punto)
    {
        if (punto == null) return;

        if (AudioManager.instancia != null)
            AudioManager.instancia.ReproducirDisparo();

        GameObject bala = Instantiate(prefabBala, punto.position, punto.rotation);
        Rigidbody rbBala = bala.GetComponent<Rigidbody>();
        rbBala.linearVelocity = punto.forward * velocidadBala;

        Bala compBala = bala.GetComponent<Bala>();
        if (compBala != null) compBala.esDelJugador = true;

        Destroy(bala, 3f);
    }

    public void ActivarDobleDisparo(float duracion)
    {
        StartCoroutine(DobleDisparoCoroutine(duracion));
    }

    IEnumerator DobleDisparoCoroutine(float duracion)
    {
        dobleDisparoActivo = true;
        yield return new WaitForSeconds(duracion);
        dobleDisparoActivo = false;
    }
}