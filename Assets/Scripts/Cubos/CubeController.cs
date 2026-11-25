using DG.Tweening.Core.Easing;
using Monou;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{

    private GameManager gameManager;
    private SoundManager soundManager;

    [Header("Movimiento")]
    public float velocidad = 15.0f;
    public float PlayerOffset = 0.5f;

    [SerializeField]
    private float distanceToPlayer, distanceToCamera;

    [Header("Tutorial y Derrota")]
    public float tutorialStopDistance = 4.0f;
    public float distanciaDerrota = 0.8f;
    public string escenaDerrota = "GameOver";

    [SerializeField]
    private bool isTutorialCube = false;
    private bool pauseTrigger = false;
    private bool yaProcesado = false;

    [Header("Puntaje")]
    public int Score = 1;

    private Transform playerTransform;
    private Transform cameraTransform;
    private Vector3 moveDirection;
    private float lifetime = 10.0f;

    public void Initialise(bool isTutorial = false)
    {
        lifetime = 10.0f;
        isTutorialCube = isTutorial;
        pauseTrigger = false;
        yaProcesado = false;

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
            Vector3 target = playerTransform.position + (Vector3.up * PlayerOffset);
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

        // 1. Pausa del tutorial frente a la c�mara
        if (isTutorialCube && !pauseTrigger && cameraTransform != null)
        {
            distanceToCamera = Vector3.Distance(transform.position, cameraTransform.position);

            if (distanceToCamera <= tutorialStopDistance)
            {
                pauseTrigger = true;

                if (PauseManager.Instance != null)
                {
                    PauseManager.Instance.TutorialPause();
                }
            }
        }

        // 2. Respaldo infalible de derrota por cercan�a al jugador
        if (playerTransform != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
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
            MouseFollower follower = other.GetComponent<MouseFollower>();
            MouseFollower.SwipeDirection dir = (follower != null)
                ? follower.CurrentDirection
                : MouseFollower.SwipeDirection.None;

            ProcesarContactoMouse(dir);
        }
        else if (other.CompareTag("Player") || other.GetComponentInParent<PlayerController>() != null)
        {
            //GameScraper.Finish(100); // marcar el Score del jugador
            ActivarDerrota();
        }
    }

    public void ProcesarContactoMouse(MouseFollower.SwipeDirection direction)
    {
        if (yaProcesado) return;

        if (direction == MouseFollower.SwipeDirection.Horizontal)
        {
            yaProcesado = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(Score);
            }
            //soundManager.PlayRandomPitch("Slash"); //No permite que se corten los cubos, AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            CortarCubo();
        }
        else if (direction == MouseFollower.SwipeDirection.Up)
        {
            yaProcesado = true;
            ActivarDerrota();
        }
    }

    private void ActivarDerrota()
    {
        Time.timeScale = 1f;
        //GameScraper.Finish(gameManager.CurrentScore); //por qué esto ya no permite que pierdas???
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
