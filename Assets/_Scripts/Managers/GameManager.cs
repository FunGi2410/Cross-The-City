using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isPauseGame;

    [SerializeField] private GameObject camOnMenu;
    [SerializeField] private GameObject camRunningGame;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        OnMenu();
    }

    public void OnMenu()
    {
        camOnMenu.SetActive(true);
        camRunningGame.SetActive(false);
        UIManager.instance.DisplayMenuUI(true);
        UIManager.instance.DisplayRunningUI(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        camOnMenu.SetActive(false);
        camRunningGame.SetActive(true);
        UIManager.instance.DisplayMenuUI(false);
        UIManager.instance.DisplayRunningUI(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("GameScene");
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
