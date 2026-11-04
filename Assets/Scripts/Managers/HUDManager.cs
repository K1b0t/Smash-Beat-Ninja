using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HScoreText;

    private void Awake()
    {

        if (GameManager.Instance == null)
        {
            Instantiate(Resources.Load("Game Manager"));
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ScoreText = ScoreText;
            GameManager.Instance.HScoreText = HScoreText;

        }

    }

    public void ChangeToScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public void Activate(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Attempted to activate a null GameObject");
        }
    }
    public void Dectivate(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Attempted to deactivate a null GameObject");
        }
    }
    public void ToggleActivate(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("Attempted to toggle a null GameObject");
        }
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

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
