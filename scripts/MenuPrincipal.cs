using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelMenu;
    public GameObject panelOpciones;

    void Start()
    {
        panelOpciones.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void AbrirOpciones()
    {
        panelMenu.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void VolverAlMenu()
    {
        panelOpciones.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }
}