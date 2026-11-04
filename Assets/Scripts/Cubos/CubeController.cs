using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float velocidad = 10.0f;
    //public Vector3 mousePosition;

    float lifetime = 5.0f;

    public void Initialise()
    {

        lifetime = 10.0f;
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if ( lifetime < 0.0f)
        {
            ObjectPooler.EnqueueObject(this, "Cube");
        }
    }
}