using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private bool isPauseGame;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        isPauseGame = true;
        Time.timeScale = 0;
        UIManager.instance.DisplayPausePanel(isPauseGame);
    }

    public void ResumeGame()
    {
        isPauseGame = false;
        Time.timeScale = 1;
        UIManager.instance.DisplayPausePanel(isPauseGame);
    }

    public void GameOver()
    {
        UIManager.instance.DisplayGameOverPanel(true, Bike.Instance.Distance, Bike.Instance.CoinNumber);
    }
}
