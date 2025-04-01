using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlayerView : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enterNamePanel;
    [SerializeField] 
    private Button _btn;
    //[SerializeField]
    //private TextMeshProUGUI _levelText;
   // private TextMeshProUGUI _showNameText;
    [SerializeField]
    private TMP_InputField _getPlayerNameText;
    private PlayerModel _playerModel;
    [SerializeField] private Image[] hearts; // Mảng chứa tất cả trái tim có thể hiển thị
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private string _playerNameText;

    public string PlayerNameText
    {
        get { return _playerNameText; }
    }
    

    private void Start()
    {
        // if (_playerModel == null)
        // {
        //     Debug.Log("PlayerStats is missing!");
        // }

        //_playerStats.LevelChanged += UpdateLevelText;
        
        UpdatePlayerNameText();
        //_playerModel = gameObject.GetComponent<PlayerModel>();;
    }

    // private void UpdateLevelText()
    // {
    //     if (_levelText != null)
    //     {
    //         _levelText.text = "Level:" + _playerStats.GetPlayerCurrentLevel();
    //     }
    // }

    private void UpdatePlayerNameText()
    {
        if (_playerNameText != null)
        {
            _playerNameText = GameManager.Instance.PlayerController.PlayerModel.GetPlayerName();
        }
    }

    // private void OnDestroy()
    // {
    //     if (_playerStats != null)
    //     {
    //         _playerStats.LevelChanged -= UpdateLevelText;
    //     }
    // }

    public void ReadNameInput()
    {
        _playerNameText = _getPlayerNameText.text;
        Debug.Log(_playerNameText);
        HideGetNameButton();
        GameManager.Instance.StartLevel();
            
    }

    public void ShowGetNameButton()
    {
        
        if (_enterNamePanel != null)
        {
            _enterNamePanel.gameObject.SetActive(true);
        }
    }
    public void HideGetNameButton()
    {
        if (_btn != null)
        {
            _enterNamePanel.gameObject.SetActive(false);
        }
    }

    public void HideAllHearts()
    {
        if(hearts == null) return;
        foreach (var heart in hearts)
        {
            if (heart != null)
            {
                heart.gameObject.SetActive(false);
            }
        }
    }

    public void ConfigureHeartUI()
    {
        if (GameManager.Instance.PlayerController == null || GameManager.Instance.PlayerController.PlayerModel == null) return;
        int maxHealth = GameManager.Instance.LevelManager.CurrentLevelConfig.maxHealth;
        for(int i=0; i<hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < maxHealth);
        }

        UpdateHealthUI();

    }

    private void UpdateHealthUI()
    {
        if (GameManager.Instance.PlayerController == null || GameManager.Instance.PlayerController.PlayerModel == null) return;
        int currentHealth = GameManager.Instance.PlayerController.PlayerModel.CurrentHealth;
        int maxHealth = GameManager.Instance.LevelManager.CurrentLevelConfig.maxHealth;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth)
            {
                if (i < currentHealth)
                {
                    hearts[i].sprite = fullHeart; // Hiển thị trái tim đầy
                }
                else
                {
                    hearts[i].sprite = emptyHeart; // Hiển thị trái tim trống
                }
                hearts[i].enabled = true;
            }
        }
    }
    // Gọi khi level thay đổi để cập nhật UI
    public void RefreshUI()
    {
        ConfigureHeartUI();

    }
    
    
}
