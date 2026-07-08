using UnityEngine;

public class PowerUpRotacion : MonoBehaviour
{
    public float velocidadRotacion = 90f; // grados por segundo

    void Update()
    {
        transform.Rotate(0f, velocidadRotacion * Time.deltaTime, 0f);
    }
}