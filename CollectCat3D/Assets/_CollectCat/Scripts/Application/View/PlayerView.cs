using System;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private TextMeshProUGUI _playerNameText;
    [SerializeField]
    private PlayerStats _playerStats;

    private void Start()
    {
        if (_playerStats == null)
        {
            Debug.Log("PlayerStats is missing!");
        }

        _playerStats.LevelChanged += UpdateLevelText;
        
        UpdatePlayerNameText();
        
    }

    private void UpdateLevelText()
    {
        if (_levelText != null)
        {
            _levelText.text = "Level:" + _playerStats.GetPlayerCurrentLevel();
        }
    }

    private void UpdatePlayerNameText()
    {
        if (_playerNameText != null)
        {
            _playerNameText.text = _playerStats.GetPlayerName();
        }
    }

    private void OnDestroy()
    {
        if (_playerStats != null)
        {
            _playerStats.LevelChanged -= UpdateLevelText;
        }
    }
}
