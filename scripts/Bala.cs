using UnityEngine;

public class Bala : MonoBehaviour
{
    public int danio = 25;
    public bool esDelJugador = true;

    void OnCollisionEnter(Collision col)
    {
        if (esDelJugador)
        {
            EnemigoCombate enemigo = col.gameObject.GetComponentInParent<EnemigoCombate>();
            if (enemigo != null) enemigo.RecibirDanio(danio);
        }
        else
        {
            VidaJugador jugador = col.gameObject.GetComponentInParent<VidaJugador>();
            if (jugador != null) jugador.RecibirDanio(danio);
        }

        Destroy(gameObject);
    }
}