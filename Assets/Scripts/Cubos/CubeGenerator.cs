using UnityEngine;

public class CubeGenerator : MonoBehaviour
{

    public CubeController cube;

    public float spawnTimer = 1.0f;

    //Porcentajes normalizados del 0 al 1, 1 = 100%, 0.9 = 90%, etc.
    [SerializeField, Range(0, 1)]
    private float spawnTimerDecreasePercent = 0.25f;

    [SerializeField, Range(0, 1)]
    //valor mínimo que pude tener el spawnTimer
    private float minSpawnTimer = 0.2f;

    //el valor que se le restará a spawn timer para acelerar el spawn de los cubos
    private float decreaseFactor;

    private float timer = 0.0f;
    private bool firstCubeSpawned = false;

    [SerializeField]
    private float radius = 50;

    void Start()
    {
        decreaseFactor = spawnTimer * spawnTimerDecreasePercent;
    }

    void Update()
    {

        if (PauseManager.Instance != null && PauseManager.Instance.isPaused)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
            SpawnCube();
            timer = 0.0f;
        }
    }

    public void DecreaseSpawnTime()
    {
        //spawnTimer = spawnTimer - decreaseFactor;
        spawnTimer -= decreaseFactor;
        spawnTimer = Mathf.Clamp(spawnTimer, minSpawnTimer, float.MaxValue);
    }

    // Update is called once per frame
    void SpawnCube()
    {
        CubeController instance = ObjectPooler.DequeueObject<CubeController>("Cube");

        Vector3 randomPos = (Random.insideUnitSphere * radius) + transform.position;
        Vector3 spawnPos = new Vector3(randomPos.x, randomPos.y, transform.position.z);


        instance.transform.position = spawnPos;
        instance.transform.rotation = transform.rotation;

        bool isTutorial = !firstCubeSpawned;
        firstCubeSpawned = true;

        instance.Initialise(isTutorial);
        instance.gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
