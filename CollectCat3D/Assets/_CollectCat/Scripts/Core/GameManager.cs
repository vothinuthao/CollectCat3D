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
    public UnityEvent<GameState> OnGameStageChanged;
    private GameState _currentGameState;
    public GameState CurrentGameState => _currentGameState;
    
    
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetGameState(GameState gameState)
    {
        _currentGameState = gameState;
        OnGameStageChanged?.Invoke(gameState);
    }
    public void GoToMainMenu()
    {
        SetGameState(GameState.MainMenu);
        
        
    }

    public void StartLevel()
    {
        SetGameState(GameState.Gameplay);
        Time.timeScale = 1f;
    }
    
    

    public void PauseGame()
    {
        if (_currentGameState == GameState.Gameplay)
        {
            SetGameState(GameState.Paused);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (_currentGameState == GameState.Paused)
        {
            SetGameState(GameState.Gameplay);
            Time.timeScale = 1f;
        }
    }

    public void EndGame(bool success)
    {
        SetGameState(success?GameState.LevelComplete:GameState.GameOver);
        Time.timeScale = 0f;
        if (!success)
        {
            SetGameState(GameState.GameOver);
        }
        else
        {
            SetGameState(GameState.Win);
        }
    }
    
 
    
    
    
}
