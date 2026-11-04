using UnityEngine;

public class MoverCubo : MonoBehaviour
{
    public float velocidad = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}