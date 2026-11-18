using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 15.0f;
    public float alturaObjetivoOffset = 0.5f;

    [Header("Tutorial y Derrota")]
    public float tutorialStopDistance = 4.0f;
    public float distanciaDerrota = 0.8f; 
    public string escenaDerrota = "GameOver";

    private bool isTutorialCube = false;
    private bool pauseTrigger = false;

    private Transform playerTransform;
    private Transform cameraTransform;
    private Vector3 moveDirection;
    private float lifetime = 10.0f;

    public void Initialise(bool isTutorial = false)
    {
        lifetime = 10.0f;
        isTutorialCube = isTutorial;
        pauseTrigger = false;

        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            playerTransform = GameManager.Instance.Player.transform;
        }

        if (playerTransform != null)
        {
            Vector3 target = playerTransform.position + (Vector3.up * alturaObjetivoOffset);
            moveDirection = (target - transform.position).normalized;
            transform.LookAt(target);
        }
        else
        {
            moveDirection = -Vector3.forward;
        }
    }

    private void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.isPaused)
            return;

        Vector3 target = transform.position + moveDirection;

        transform.Translate(moveDirection * Mathf.Abs(velocidad) * Time.deltaTime, Space.World);

        // 1. Pausa del tutorial frente a la cámara
        if (isTutorialCube && !pauseTrigger && cameraTransform != null)
        {
            float distanceToCamera = Vector3.Distance(transform.position, cameraTransform.position);

            if (distanceToCamera <= tutorialStopDistance)
            {
                pauseTrigger = true;

                if (PauseManager.Instance != null)
                {
                    PauseManager.Instance.TutorialPause();
                }
            }
        }

        // 2. Respaldo infalible de derrota por cercanía al jugador
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target);
            if (distanceToPlayer <= distanciaDerrota)
            {
                ActivarDerrota();
            }
        }

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
            CortarCubo();
        }
        else if (other.CompareTag("Player") || other.GetComponentInParent<PlayerController>() != null)
        {
            ActivarDerrota();
        }
    }

    private void ActivarDerrota()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaDerrota);
    }

    public void CortarCubo()
    {
        if (isTutorialCube)
        {
            isTutorialCube = false;
            if (PauseManager.Instance != null)
            {
                PauseManager.Instance.CompleteTutorial();
            }
        }

        ObjectPooler.EnqueueObject(this, "Cube");
    }
}