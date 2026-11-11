using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    public float distanceFromCamera = 10.0f;

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.z = distanceFromCamera;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        transform.position = worldPosition;
    }
}