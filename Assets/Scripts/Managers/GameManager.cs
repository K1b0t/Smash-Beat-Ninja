using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HScoreText;


    public PlayerController Player;

    public CubeController cubePrefab;


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

        SetupPool();

    }

    private void SetupPool()
    {
        ObjectPooler.SetupPool(cubePrefab, 10, "Cube");
    }

    public void ReflectScore(float CurrentScore)
    {
        if (ScoreText != null)
            ScoreText.text = "Score: " + CurrentScore.ToString("0.0");
    }

    public void ReflectHScore(int CurrentHScore)
    {
        if (HScoreText != null)
            HScoreText.text = "High Score: " + CurrentHScore.ToString("0");
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            Player = player;
        }
    }



}
