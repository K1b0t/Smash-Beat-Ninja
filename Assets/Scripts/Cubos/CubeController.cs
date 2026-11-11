using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float velocidad = -15.0f;

    float lifetime = 5.0f;

    public void Initialise()
    {
        lifetime = 10.0f;
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime < 0.0f)
        {
            ObjectPooler.EnqueueObject(this, "Cube");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Mouse_Track" || other.CompareTag("MouseTrack"))
        {
            ObjectPooler.EnqueueObject(this, "Cube");
        }
    }
}