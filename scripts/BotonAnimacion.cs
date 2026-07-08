using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAnimacion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Zoom")]
    public float escalaHover = 1.1f;
    public float escalaClick = 0.95f;
    public float velocidad = 8f;

    Vector3 escalaOriginal;
    Vector3 escalaObjetivo;

    void Awake()
    {
        escalaOriginal = transform.localScale;
        escalaObjetivo = escalaOriginal;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, velocidad * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        escalaObjetivo = escalaOriginal * escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        escalaObjetivo = escalaOriginal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        escalaObjetivo = escalaOriginal * escalaClick;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        escalaObjetivo = escalaOriginal * escalaHover;
    }
}