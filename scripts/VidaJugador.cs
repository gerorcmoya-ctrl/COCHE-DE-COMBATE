using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int danioColision = 10;
    int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        UIManager.instancia.ActualizarCorazones(vidaActual, vidaMaxima);
    }

    public void RecibirDanio(int danio)
    {
        vidaActual -= danio;
        vidaActual = Mathf.Max(0, vidaActual);
        UIManager.instancia.ActualizarCorazones(vidaActual, vidaMaxima);
        if (vidaActual <= 0) Morir();
    }
    public void RestablecerVida()
    {
        vidaActual = vidaMaxima;
        UIManager.instancia.ActualizarCorazones(vidaActual, vidaMaxima);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponentInParent<EnemigoCombate>() != null)
        {
            RecibirDanio(danioColision);
        }
    }

    void Morir()
    {
        UIManager.instancia.MostrarGameOver();
        gameObject.SetActive(false);
    }
}