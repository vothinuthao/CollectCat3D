using UnityEngine;
using UnityEngine.UI;
public class EndGameUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public Button playAgainButton;
    public Button restartButton;
    public Button menuButton;

    void Start()
    {
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgainBtn);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartBtn);
        }

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(MenuBtn);
        }
        
    }
    public void PlayAgainBtn()
    {
       HideGameOverPanel();
       GameManager.Instance.RestartLevel();
    }

    public void RestartBtn()
    {
        HideGameOverPanel();
        GameManager.Instance.GoToMainMenu();
    }

    public void MenuBtn()
    {
        HideGameWinPanel();
        GameManager.Instance.GoToMainMenu();
    }
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameWinPanel()
    {
        gameWinPanel.SetActive(true);
    }

    public void HideGameWinPanel()
    {
        gameWinPanel.SetActive(false);
    }
}
