using UnityEngine;
using UnityEngine.UI;
public class LevelCompleteUI : MonoBehaviour
{
    public GameObject LevelCompletePanel;
    public Button nextLevelButton;
    public Button playAgainButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelCompletePanel.SetActive(false);
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelBtnClicked);
        }

        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(OnPlayAgainBtnClicked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayAgainBtnClicked()
    {
        LevelCompletePanel.SetActive(false);
        GameManager.Instance.RestartLevel();
    }

    private void OnNextLevelBtnClicked()
    {
        LevelCompletePanel.SetActive(false);
        GameManager.Instance.LoadNextLevel();
    }

    public void ShowLevelCompletePanel()
    {
        LevelCompletePanel.SetActive(true);
    }
}
