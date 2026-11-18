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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            CubeController cube = hit.collider.GetComponentInParent<CubeController>();
            if (cube != null)
            {
                cube.CortarCubo();
            }
        }
    }
}