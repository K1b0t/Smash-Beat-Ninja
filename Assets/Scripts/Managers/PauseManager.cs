using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public bool isPaused { get; private set; }
    public bool firstCube { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        firstCube = false;
        Time.timeScale = 1f;
    }

    public void TutorialPause()
    {
        isPaused = true;
        firstCube = true;
        Time.timeScale = 1f;

        Debug.Log("Corta el cubooooo");
    }

    public void CompleteTutorial()
    {
        if (firstCube)
        {
            UnpauseGame();
            Debug.Log("Empieza el genocido");
        }
    }
}
