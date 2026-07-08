using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TipoPowerUp { DobleDisparo, Velocidad, Vida }

    [Header("Tipo")]
    public TipoPowerUp tipo;

    [Header("Duración")]
    public float duracion = 5f;

    void OnTriggerEnter(Collider col)
    {
        VidaJugador jugador = col.GetComponentInParent<VidaJugador>();
        if (jugador == null) return;

        switch (tipo)
        {
            case TipoPowerUp.DobleDisparo:
                DisparoVehiculo disparo = col.GetComponentInParent<DisparoVehiculo>();
                if (disparo != null) disparo.ActivarDobleDisparo(duracion);
                break;

            case TipoPowerUp.Velocidad:
                ControlVehiculo control = col.GetComponentInParent<ControlVehiculo>();
                if (control != null) control.ActivarVelocidad(duracion);
                break;

            case TipoPowerUp.Vida:
                jugador.RestablecerVida();
                break;
        }

        if (AudioManager.instancia != null)
            AudioManager.instancia.ReproducirPowerUp();

        Destroy(gameObject);
    }
}