using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public static PauseManager Instance;

    public bool IsPaused {  get; private set; }

    public float TutorialCountdown = 5f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void UnpauseGame ()
    {
        IsPaused = false;
        Time.timeScale = 1f;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
