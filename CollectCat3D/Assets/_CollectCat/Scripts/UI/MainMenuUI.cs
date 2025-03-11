using Unity.Properties;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        mainMenuPanel.SetActive(false);
        GameManager.Instance.StartLevel();
    }
}
