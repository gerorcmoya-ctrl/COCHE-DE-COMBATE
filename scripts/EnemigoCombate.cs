using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemigoCombate : MonoBehaviour
{
    [Header("Movimiento")]
    public float fuerzaAvance = 800f;
    public float fuerzaGiro = 100f;
    public float velocidadMaxima = 12f;

    [Header("Combate")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 25f;
    public float cadencia = 1.5f;

    [Header("Vida")]
    public int vidaMaxima = 100;
    int vidaActual;

    Rigidbody rb;
    Transform jugador;
    float tiempoUltimoDisparo;
    float giroActual;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vidaActual = vidaMaxima;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Jugador");
        if (obj != null) jugador = obj.transform;
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        PerseguirJugador();
        IntentarDisparar();
    }

    void PerseguirJugador()
    {
        // Mover directo hacia el jugador sin rotación del rigidbody
        Vector3 dirFlat = jugador.position - transform.position;
        dirFlat.y = 0f;
        dirFlat.Normalize();

        if (rb.linearVelocity.magnitude < velocidadMaxima)
        {
            rb.AddForce(dirFlat * fuerzaAvance * Time.fixedDeltaTime);
        }

        // Rotar el modelo hijo para que mire hacia donde va
        if (dirFlat != Vector3.zero)
        {
            Quaternion rotObjetivo = Quaternion.LookRotation(dirFlat);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, rotObjetivo, 5f * Time.fixedDeltaTime));
        }
    }

    void IntentarDisparar()
    {
        if (prefabBala == null || puntoDisparo == null) return;
        if (Time.time < tiempoUltimoDisparo + cadencia) return;

        tiempoUltimoDisparo = Time.time;

        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rbBala = bala.GetComponent<Rigidbody>();
        if (rbBala != null) rbBala.linearVelocity = puntoDisparo.forward * velocidadBala;

        Bala compBala = bala.GetComponent<Bala>();
        if (compBala != null) compBala.esDelJugador = false;

        Destroy(bala, 3f);
    }

    public void RecibirDanio(int danio)
    {
        vidaActual -= danio;
        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        if (AudioManager.instancia != null)
            AudioManager.instancia.ReproducirMuerteEnemigo();

        if (UIManager.instancia != null)
            UIManager.instancia.SumarKill();

        if (GestorFases.instancia != null)
            GestorFases.instancia.EnemigoEliminado();

        Destroy(gameObject);
    }
}