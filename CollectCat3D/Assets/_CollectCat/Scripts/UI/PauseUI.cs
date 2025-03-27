using UnityEngine;
using UnityEngine.UI;
public class PauseUI : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button resumeButton;
    public Button restartButton;
    public Button mainMenu;
    void Start()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(OnResumeClicked);
        }

        if (mainMenu != null)
        {
            mainMenu.onClick.AddListener(OnMainMenuClicked);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrentGameState == GameManager.GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }

    private void PauseGame()
    {
        GameManager.Instance.PauseGame();
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    private void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    private void OnMainMenuClicked()
    {
        pauseMenuPanel.SetActive(false);
        GameManager.Instance.GoToMainMenu();
    }

    private void OnResumeClicked()
    {
        pauseMenuPanel.SetActive(false);
        ResumeGame();
        
    }

    private void OnRestartClicked()
    {
        pauseMenuPanel.SetActive(false);
        GameManager.Instance.RestartLevel();
        
    }
    
}
