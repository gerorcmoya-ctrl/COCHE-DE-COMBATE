using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Corazones")]
    public Image corazon1;
    public Image corazon2;
    public Image corazon3;
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    [Header("Paneles")]
    public GameObject panelGameOver;
    public GameObject panelVictoria;

    [Header("Kills")]
    public TMPro.TextMeshProUGUI textoKills;
    int kills = 0;

    public static UIManager instancia;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        panelGameOver.SetActive(false);
        panelVictoria.SetActive(false);
        textoKills.text = "Kills: 0";
    }

    public void ActualizarCorazones(int vidaActual, int vidaMaxima)
    {
        int tercio = vidaMaxima / 3;

        corazon3.sprite = vidaActual >= tercio * 3 ? corazonLleno : corazonVacio;
        corazon2.sprite = vidaActual >= tercio * 2 ? corazonLleno : corazonVacio;
        corazon1.sprite = vidaActual >= tercio * 1 ? corazonLleno : corazonVacio;
    }

    public void SumarKill()
    {
        kills++;
        textoKills.text = "Kills: " + kills;
    }

    public void MostrarGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MostrarVictoria()
    {
        panelVictoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;

        if (AudioManager.instancia != null)
        {
            AudioManager.instancia.musicaFondo.Stop();
            AudioManager.instancia.musicaFondo.Play();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;

        if (AudioManager.instancia != null)
        {
            AudioManager.instancia.musicaFondo.Stop();
            AudioManager.instancia.musicaFondo.Play();
        }

        SceneManager.LoadScene(0);
    }
}