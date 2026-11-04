using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(Resources.Load("Game Manager"));
        }

        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            GameManager.Instance.Player = player;
        }

        HUDManager hud = FindFirstObjectByType<HUDManager>();

        if (hud != null)
        {
            GameManager.Instance.ScoreText = hud.ScoreText;
            GameManager.Instance.HScoreText = hud.HScoreText;
        }

    }

}
