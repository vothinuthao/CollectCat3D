using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enterNamePanel;
    [SerializeField] 
    private Button _btn;
    [SerializeField]
    //private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _showNameText;
    [SerializeField]
    private TMP_InputField _getPlayerNameText;
    private PlayerModel _playerStats;

    private string _playerNameText;

    private void Start()
    {
        if (_playerStats == null)
        {
            Debug.Log("PlayerStats is missing!");
        }

        //_playerStats.LevelChanged += UpdateLevelText;
        
        UpdatePlayerNameText();
        _playerStats = gameObject.GetComponent<PlayerModel>();;
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
            _playerNameText = _playerStats.GetPlayerName();
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
    
}
