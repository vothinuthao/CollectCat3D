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

    //public UnityEvent Action LevelComplete;
    private GameState _currentGameState;
    public GameState CurrentGameState => _currentGameState;
    // [SerializeField]
    // private PlayerController _playerController;
    [SerializeField]
    private EnemiesManager _enemiesManager;
    [SerializeField]
    private ItemController _itemController;   
    [SerializeField]
    private LevelManager _levelManager;
    [SerializeField]
    private InventoryController _inventoryController;
    [SerializeField]
    private MapManager _mapManager;
    [SerializeField]
    private LevelData _levelData;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private ItemView _itemView;
    // [SerializeField]
    // private EnemyView _enemyView;
    [SerializeField]
    private MainMenuUI _mainMenuUI;
    [SerializeField]
    private LevelCompleteUI _levelCompleteUI;
    [SerializeField]
    private EndGameUI _endGameUI;
    [SerializeField]
    private PauseUI _pauseUI;
    private ItemSO _itemData;
    
    private Bounds _mapBounds;



    public InventoryController InventoryController => _inventoryController;
   
    
   void Start()
{ 
    // if (_playerController == null)
    // {
    //     _playerController = GetComponent<PlayerController>();
    //     if (_playerController == null)
    //     {
    //         Debug.LogError("_playerController is missing! Make sure it is attached.");
    //     }
    // }

    // if (_inventoryController == null)
    // {
    //     _enemyController = GetComponent<EnemyController>();
    //     if (_enemyController == null)
    //     {
    //         Debug.LogError("_enemyController is missing! Make sure it is attached.");
    //     }
    // }
    //
    //
    // if (_itemController == null)
    // {
    //     _itemController = GetComponent<ItemController>();
    //     if (_itemController == null)
    //     {
    //         Debug.LogError("_itemController is missing! Make sure it is attached.");
    //     }
    // }

    if (_levelManager == null)
    {
        _levelManager = GetComponent<LevelManager>();
        if (_levelManager == null)
        {
            Debug.LogError("_levelManager is missing! Make sure it is attached.");
        }
    }

    if (_inventoryController == null)
    {
        _inventoryController = GetComponent<InventoryController>();
        if (_inventoryController == null)
        {
            Debug.LogError("_inventoryController is missing! Make sure it is attached.");
        }
    }

    if (_mapManager == null)
    {
        _mapManager = GetComponent<MapManager>();
        if (_mapManager == null)
        {
            Debug.LogError("_mapManager is missing! Make sure it is attached.");
        }
    }

    if (_playerView == null)
    {
        _playerView = GetComponent<PlayerView>();
        if (_playerView == null)
        {
            Debug.LogError("_playerView is missing! Make sure it is attached.");
        }
    }

    if (_itemView == null)
    {
        _itemView = GetComponent<ItemView>();
        if (_itemView == null)
        {
            Debug.LogError("_itemView is missing! Make sure it is attached.");
        }
    }
    

    // _enemyView = GetComponent<EnemyView>();
    // if (_enemyView == null)
    // {
    //     Debug.LogError("_enemyView is missing! Make sure it is attached.");
    // }

    if (_mainMenuUI == null)
    {
        _mainMenuUI = GetComponent<MainMenuUI>();
        if (_mainMenuUI == null)
        {
            Debug.LogError("_mainMenuUI is missing! Make sure it is attached.");
        }
    }

    if (_levelCompleteUI == null)
    {
        _levelCompleteUI = GetComponent<LevelCompleteUI>();
        if (_levelCompleteUI == null)
        {
            Debug.LogError("_levelCompleteUI is missing! Make sure it is attached.");
        }
    }


    if (_endGameUI == null)
    {
        _endGameUI = GetComponent<EndGameUI>();
        if (_endGameUI == null)
        {
            Debug.LogError("_endGameUI is missing! Make sure it is attached.");
        }
    }

    
    // _itemBase = GetComponent<ItemBase>();
    // if (_itemBase == null)
    // {
    //     Debug.LogError("_itemBase is missing! Make sure it is attached.");
    // }

    
    if (_itemController != null)
    {
        _itemController.Initialize();
    }

    if (_inventoryController != null && _itemData != null)
    {
        _inventoryController.Initialize();
    }

    if (_levelManager != null && _itemController != null && _inventoryController != null)
    {
        _levelManager.Initialize();
    }
    else
    {
        Debug.LogError("null");
    }

    Time.timeScale = 0f;
    GoToMainMenu();
   // PlayerViewInit();
}


    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameState(GameState gameState)
    {
        _currentGameState = gameState;
        
    }
    public void GoToMainMenu()
    {
        SetGameState(GameState.MainMenu);
        _mainMenuUI.ShowMainMenu();
        

    }

    public void StartLevel()
    {
        SetGameState(GameState.Gameplay);
        Time.timeScale = 1f;
        _levelManager.LoadLevel(1);
        InitializeEnemies();
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

    public void RestartLevel()
    {
        if (_currentGameState == GameState.Paused || _currentGameState == GameState.GameOver || _currentGameState == GameState.LevelComplete)
        {
            _enemiesManager.ClearEnemies();
          _levelManager.ReloadLevel();
          InitializeEnemies();
          SetGameState(GameState.Gameplay);
          
        }
    }

    public void EndGame(bool success)
    {
        SetGameState(success?GameState.Win:GameState.GameOver);
        Time.timeScale = 0f;
        if (!success)
        {
            _endGameUI.ShowGameOverPanel();
        }
        else
        {
            _endGameUI.ShowGameWinPanel();
        }
    }

    public void OnLevelComplete()
    {
        if (_currentGameState == GameState.Gameplay && _inventoryController.TotalCollectableQuantity() == _levelManager.CurrentLevelConfig.requiredItems)
        {
            SetGameState(GameState.LevelComplete);
            Time.timeScale = 0f;
            _levelCompleteUI.ShowLevelCompletePanel();
        }
    }

    public void LoadNextLevel()
    {
        _levelManager.LoadNextLevel();
        InitializeEnemies();
    }
    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {
        
    }

    public void OnPlayButtonPressed()
    {
        if (_playerView.PlayerNameText == null)
        {
            _playerView.ShowGetNameButton();
        }
        else
        {
            StartLevel();
        }
    }

    public void ResetData()
    {
        _inventoryController.ResetData();
    }

    public void InitializeGround()
    {

        // Lấy bounds từ collider của ground
        Collider groundCollider = _levelManager.MapData.ground.GetComponent<Collider>();
        if (groundCollider != null)
        {
            _mapBounds = groundCollider.bounds;
            Debug.Log($"Ground bounds: {_mapBounds.min} to {_mapBounds.max}");
        }
        else
        {
            Debug.LogError("Ground không có Collider!");
        }
        
    }
    public void SpawnItem(LevelData levelData)
    {
        if (_mapBounds.size == Vector3.zero)
        {
            Debug.LogError("GameManager: Map bounds chưa được khởi tạo!");
            return;
        }

        if (_itemController == null)
        {
            Debug.LogError("GameManager: _itemController is null!");
        }
        else
        {
            _itemController.SetupCollectableSpawn(_mapBounds,_levelManager.CurrentLevelConfig.requiredItems, _levelManager.CurrentLevelConfig.minSpawnSpacing);
        }
        
    }

    public void InitializeEnemies()
    {
        _enemiesManager.InitializeEnemyDate();
    }

    // public void PlayerViewInit()
    // {
    //     _playerView.Init();
    // }
    
    

}
