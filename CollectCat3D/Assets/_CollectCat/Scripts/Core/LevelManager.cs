using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public LevelData levelData;
    private int currentLevel =0 ;
    private LevelConfig currentLevelConfig;
    public MapManager mapManager;
    private MapData mapData;
    private Bounds _mapBounds;
   

   
    public LevelConfig CurrentLevelConfig
    {
        get { return currentLevelConfig; }
    }

    public MapData MapData
    {
        get { return mapData; }
    }

    public void Initialize()
    {
        if (levelData == null)
        {
            Debug.Log("no level data loaded");
        }
       
    }
    private void SetCurrentLevelIndex()
    {
        currentLevelConfig = levelData.listLevelConfig.FirstOrDefault(x => x.levelNumber == currentLevel);
        if (currentLevelConfig == null)
        {
            Debug.Log("no level config found");
        }
        mapData = mapManager.listMaps.FirstOrDefault(x=>x.mapId==currentLevel);
        if (mapData == null)
        {
            Debug.Log("no map data found");
        }
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        SetCurrentLevelIndex();
        if (currentLevelConfig != null)
        {
            LoadCurrentLevel();
        }
    }

    public void LoadCurrentLevel()
    {
        if (currentLevelConfig == null)
        {
            Debug.Log("no level config loaded");
        }
        Debug.Log($"Loaading Level : {currentLevelConfig}");

        GameManager.Instance.ResetData();
       

        Time.timeScale = 1f;
        mapManager.LoadMap(currentLevelConfig.levelNumber);
        GameManager.Instance.InitializeGround();
        GameManager.Instance.SpawnItem(levelData);



    }

    public void LoadNextLevel()
    {
        if (currentLevel + 1 < levelData.listLevelConfig.Count)
        {
            LoadLevel(currentLevel + 1);
        }
        else
        {
            Debug.Log("no next level loaded");
            GameManager.Instance.EndGame(success: true);
        }
    }

    public void ReloadLevel()
    {
        Debug.Log($"Reloading Level {currentLevelConfig.levelNumber}");
        GameManager.Instance.ResetData();
        Time.timeScale = 1f;
        mapManager.LoadMap(currentLevelConfig.levelNumber);
        GameManager.Instance.SpawnItem(levelData);
      
    }
   
    
    
    
}
