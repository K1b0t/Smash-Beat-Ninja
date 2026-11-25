using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    public float distanceFromCamera = 10.0f;

    [Header("Sensibilidad de Corte")]
    [Tooltip("Píxeles mínimos que debe moverse el mouse en un frame para contar como movimiento.")]
    public float minMovementThreshold = 1.5f;

    private Vector3 lastMousePosition;

    public enum SwipeDirection
    {
        None,
        Horizontal,
        Up,
        Down
    }

    public SwipeDirection CurrentDirection { get; private set; } = SwipeDirection.None;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Calculamos el desplazamiento del mouse en el último frame
        Vector3 delta = mouseScreenPosition - lastMousePosition;
        lastMousePosition = mouseScreenPosition;

        EvaluarDireccion(delta);

        // Actualizamos la posición de Mouse_Track en el mundo
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        transform.position = worldPosition;

        // Detección por Raycast hacia los cubos
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            CubeController cube = hit.collider.GetComponentInParent<CubeController>();
            if (cube != null)
            {
                cube.ProcesarContactoMouse(CurrentDirection);
            }
        }
    }

    private void EvaluarDireccion(Vector3 delta)
    {
        float absX = Mathf.Abs(delta.x);
        float absY = Mathf.Abs(delta.y);

        // Si el mouse apenas se movió, no registramos dirección
        if (absX < minMovementThreshold && absY < minMovementThreshold)
        {
            CurrentDirection = SwipeDirection.None;
            return;
        }

        // Si el componente vertical (Y) supera al horizontal (X)
        if (absY > absX)
        {
            CurrentDirection = (delta.y > 0f) ? SwipeDirection.Up : SwipeDirection.Down;
        }
        else
        {
            // Izquierda o derecha
            CurrentDirection = SwipeDirection.Horizontal;
        }
    }
}