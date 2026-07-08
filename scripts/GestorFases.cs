using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestorFases : MonoBehaviour
{
    public static GestorFases instancia;

    [System.Serializable]
    public class Fase
    {
        public string nombreFase;
        public Transform[] puntosSpawn;
        public GameObject puerta;
        public Transform spawnPowerUpDisparo;
        public Transform spawnPowerUpVelocidad;
        public Transform spawnPowerUpVida;
    }

    [Header("Configuración")]
    public GameObject prefabEnemigo;
    public Fase[] fases;

    [Header("Power Ups")]
    public GameObject prefabPowerUpDisparo;
    public GameObject prefabPowerUpVelocidad;
    public GameObject prefabPowerUpVida;

    int faseActual = 0;
    int enemigosVivos = 0;
    List<GameObject> enemigosActivos = new List<GameObject>();

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        foreach (Fase fase in fases)
        {
            if (fase.puerta != null)
                fase.puerta.SetActive(true);
        }

        IniciarFase(0);
    }

    void IniciarFase(int indiceFase)
    {
        faseActual = indiceFase;
        enemigosActivos.Clear();
        enemigosVivos = 0;

        Fase fase = fases[indiceFase];

        if (indiceFase > 0 && fases[indiceFase - 1].puerta != null)
            fases[indiceFase - 1].puerta.SetActive(false);

        foreach (Transform punto in fase.puntosSpawn)
        {
            GameObject enemigo = Instantiate(prefabEnemigo, punto.position, punto.rotation);
            enemigosActivos.Add(enemigo);
            enemigosVivos++;
        }

        Debug.Log($"Fase {indiceFase + 1} iniciada — {enemigosVivos} enemigos");
    }

    public void EnemigoEliminado()
    {
        enemigosVivos--;
        Debug.Log($"Enemigos restantes: {enemigosVivos}");

        if (enemigosVivos <= 0)
        {
            StartCoroutine(SiguienteFase());
        }
    }

    IEnumerator SiguienteFase()
    {
        yield return new WaitForSeconds(1f);

        int siguienteFase = faseActual + 1;

        if (siguienteFase < fases.Length)
        {
            SpawnearPowerUps(faseActual);
            IniciarFase(siguienteFase);
        }
        else
        {
            UIManager.instancia.MostrarVictoria();
        }
    }

    void SpawnearPowerUps(int indiceFase)
    {
        Fase fase = fases[indiceFase];

        if (prefabPowerUpDisparo != null && fase.spawnPowerUpDisparo != null)
            Instantiate(prefabPowerUpDisparo, fase.spawnPowerUpDisparo.position, Quaternion.identity);

        if (prefabPowerUpVelocidad != null && fase.spawnPowerUpVelocidad != null)
            Instantiate(prefabPowerUpVelocidad, fase.spawnPowerUpVelocidad.position, Quaternion.identity);

        if (prefabPowerUpVida != null && fase.spawnPowerUpVida != null)
            Instantiate(prefabPowerUpVida, fase.spawnPowerUpVida.position, Quaternion.identity);
    }
}