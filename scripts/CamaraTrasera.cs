using UnityEngine;

public class CamaraTrasera : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform objetivo;

    [Header("Posición")]
    public float distancia = 8f;
    public float altura = 4f;
    public float suavidad = 5f;

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posDeseada = objetivo.position
                           - objetivo.forward * distancia
                           + Vector3.up * altura;

        transform.position = Vector3.Lerp(transform.position, posDeseada, suavidad * Time.deltaTime);
        transform.LookAt(objetivo.position + Vector3.up * 1f);
    }
}