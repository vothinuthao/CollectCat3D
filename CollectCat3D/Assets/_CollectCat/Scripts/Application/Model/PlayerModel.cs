using System;
using UnityEngine;

public class PlayerModel
{
    private string _playerName;
    private int _playerCurrentLevel;
    private int _playerPass;
    private int _currentHealth;
    private const int _maxHealth = 100;
    private const int _minHealth = 0;
    private const float _maxSpeed = 20f;
    private float _currentSpeed;
    public event Action HealthChanged;
    public event Action LevelChanged;
    
    public int CurrentHealth {get => _currentHealth; set => _currentHealth = value; }
    public int MinHealth => _minHealth;
    public int MaxHealth => _maxHealth;
    public float CurrentSpeed {get => _currentSpeed;  private set => _currentSpeed = value; }
    public int PlayerCurrentLevel { get => _playerCurrentLevel; set => _playerCurrentLevel = value; }

 public void IncrementHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        UpdateHealth();
    }
 
    public void DecrementHealth(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        UpdateHealth();
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        HealthChanged?.Invoke();
    }
    public void IncrementSpeed(int amount)
    {
        _currentSpeed += amount;
    }

    public void ResetSpeed()
    {
        _currentSpeed = 12f;
    }

    public void SetPlayerName(string playerName)
    {
        _playerName = playerName;
    }

    public string GetPlayerName()
    {
        return _playerName;
    }

    public void SetCurrentLevel(int level)
    {
        _playerCurrentLevel = level;
        LevelChanged?.Invoke();
    }

    public int GetPlayerCurrentLevel()
    {
        return _playerCurrentLevel;
    }

}
