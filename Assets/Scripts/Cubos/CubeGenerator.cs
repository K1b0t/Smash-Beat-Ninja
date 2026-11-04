using UnityEngine;

public class CubeGenerator : MonoBehaviour
{

    public CubeController cube;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cube==null)
        {
            CubeController instance = ObjectPooler.DequeueObject<CubeController>("Cube");
            instance.gameObject.SetActive(true);
            instance.Initialise();
            //instance.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
    }
}
