using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;
    public Button playButton;
    public Button howToPlayButton;
    public Button exitButton;

    private void Start()
    {
        HideHowToPlay();
        if (playButton != null)
        {
            playButton.onClick.AddListener(StartGame);
        }

        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(ShowHowToPlay);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(HideHowToPlay);
        }
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    private void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    private void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    public void HideMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    private void StartGame()
    {
        HideMainMenu();
        GameManager.Instance.OnPlayButtonPressed();
    }
    
}
