using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [Header("Fuentes de Audio")]
    public AudioSource musicaFondo;
    public AudioSource sonidoDisparo;
    public AudioSource sonidoPowerUp;
    public AudioSource sonidoMuerteEnemigo;

    [Header("Volumenes")]
    [Range(0f, 1f)] public float volumenMusica = 1f;
    [Range(0f, 1f)] public float volumenDisparo = 1f;
    [Range(0f, 1f)] public float volumenPowerUp = 1f;
    [Range(0f, 1f)] public float volumenMuerte = 1f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CargarVolumenes();
        if (musicaFondo != null)
        {
            musicaFondo.volume = volumenMusica;
            musicaFondo.Play();
        }
    }

    public void ReproducirDisparo()
    {
        if (sonidoDisparo != null) sonidoDisparo.PlayOneShot(sonidoDisparo.clip, volumenDisparo);
    }

    public void ReproducirPowerUp()
    {
        if (sonidoPowerUp != null) sonidoPowerUp.PlayOneShot(sonidoPowerUp.clip, volumenPowerUp);
    }

    public void ReproducirMuerteEnemigo()
    {
        if (sonidoMuerteEnemigo != null) sonidoMuerteEnemigo.PlayOneShot(sonidoMuerteEnemigo.clip, volumenMuerte);
    }

    public void CambiarVolumenMusica(float valor)
    {
        volumenMusica = valor;
        if (musicaFondo != null) musicaFondo.volume = valor;
        PlayerPrefs.SetFloat("VolMusica", valor);
    }

    public void CambiarVolumenDisparo(float valor)
    {
        volumenDisparo = PlayerPrefs.GetFloat("VolDisparo", 0.3f); 
        PlayerPrefs.SetFloat("VolDisparo", valor);
    }

    public void CambiarVolumenPowerUp(float valor)
    {
        volumenPowerUp = valor;
        PlayerPrefs.SetFloat("VolPowerUp", valor);
    }

    public void CambiarVolumenMuerte(float valor)
    {
        volumenMuerte = valor;
        PlayerPrefs.SetFloat("VolMuerte", valor);
    }

    void CargarVolumenes()
    {
        volumenMusica = PlayerPrefs.GetFloat("VolMusica", 0.3f); 
        volumenDisparo = PlayerPrefs.GetFloat("VolDisparo", 1f);
        volumenPowerUp = PlayerPrefs.GetFloat("VolPowerUp", 1f);
        volumenMuerte = PlayerPrefs.GetFloat("VolMuerte", 1f);

        if (musicaFondo != null) musicaFondo.volume = volumenMusica;
    }
}