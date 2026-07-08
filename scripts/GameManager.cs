using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    void Awake()
    {
        instancia = this;
    }
}