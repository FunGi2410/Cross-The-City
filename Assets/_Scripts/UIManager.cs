using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    /*------------ PANEL ---------------------*/
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        /*---------- DeActive Panel ----------*/
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void DisplayPausePanel(bool state)
    {
        pausePanel.SetActive(state);
    }

    public void DisplayGameOverPanel(bool state, int distance, int coin)
    {
        scoreText.text = distance + " m";
        coinText.text = coin.ToString();
        gameOverPanel.SetActive(state);
    }
}
