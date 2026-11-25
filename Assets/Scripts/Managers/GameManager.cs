using Monou;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HScoreText;

    [Header("Score Info")]
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    public PlayerController Player;

    public CubeController cubePrefab;


    //Timer cubos
    [SerializeField]
    private float timeToAccelerateCubes = 3;

    [SerializeField]
    private float timerToAccelerateCubes = 0;

    [SerializeField]
    private CubeGenerator[] cubeGenerators;


    public static GameManager Instance => m_instance;
    private static GameManager m_instance;

    float savedHealth;
    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        SetupPool();
        timerToAccelerateCubes = 0;
    }

    private void SetupPool()
    {
        ObjectPooler.SetupPool(cubePrefab, 10, "Cube");
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
        ReflectScore(CurrentScore);

        if (CurrentScore > HighScore)
        {
            HighScore = Mathf.FloorToInt(CurrentScore);
            PlayerPrefs.SetInt("HighScore", HighScore);
            ReflectHScore(HighScore);
        }
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        //GameScraper.Finish(CurrentScore); // marcar el Score del jugador
        ReflectScore(CurrentScore);
        ReflectHScore(HighScore);
    }

    public void ReflectScore(int CurrentScore)
    {
        if (ScoreText != null)
            GameScraper.Advance(CurrentScore);
        ScoreText.text = "Score: " + CurrentScore.ToString("0");
    }

    public void ReflectHScore(int CurrentHScore)
    {
        if (HScoreText != null)
            HScoreText.text = "High Score: " + CurrentHScore.ToString("0");
    }




    void Start()
    {



    }

    void Update()
    {
        CubeTimer();
    }

    private void CubeTimer()
    {
        timerToAccelerateCubes += Time.deltaTime;

        if (timerToAccelerateCubes >= timeToAccelerateCubes)
        {
            for (int i = 0; i < cubeGenerators.Length; i++)
            {
                cubeGenerators[i]?.DecreaseSpawnTime();
            }

            timerToAccelerateCubes = 0;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            Player = player;
        }

        ResetScore();
        SetupPool();
    }



}
