using UnityEngine;

public class CubeGenerator : MonoBehaviour
{

    public CubeController cube;

    public float spawnTimer = 1.0f;

    private float timer = 0.0f;
    private bool firstCubeSpawned = false;

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

    // Update is called once per frame
    void SpawnCube()
    {
        CubeController instance = ObjectPooler.DequeueObject<CubeController>("Cube");
        instance.transform.position = transform.position;
        instance.transform.rotation = transform.rotation;

        bool isTutorial = !firstCubeSpawned;
        firstCubeSpawned = true;

        instance.Initialise(isTutorial);
        instance.gameObject.SetActive(true);
    }
}
