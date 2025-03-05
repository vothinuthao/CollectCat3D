using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MainMenu,
        Gameplay,
        GameOver,
        Paused,
        LevelComplete,
        Win
    }
    public event UnityAction<GameState> OnGameStageChanged;
    private GameState _currentGameState;
    public GameState CurrentGameState => _currentGameState;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        SetGameState(GameState.MainMenu);
    }

    public void StartLevel()
    {
        SetGameState(GameState.Gameplay);
    }
    

    private void SetGameState(GameState gameState)
    {
        _currentGameState = gameState;
        OnGameStageChanged?.Invoke(gameState);
    }

    public void PauseGame()
    {
        if (_currentGameState == GameState.Gameplay)
        {
            SetGameState(GameState.Paused);
        }
    }

    public void ResumeGame()
    {
        if (_currentGameState == GameState.Paused)
        {
            SetGameState(GameState.Gameplay);
        }
    }
    
}
