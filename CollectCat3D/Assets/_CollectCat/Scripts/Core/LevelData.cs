using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class LevelConfig
{
    public string levelName;
    public int levelNumber;
    public string levelDescription;
    public int maxHealth;
    public int maxSpawn;
    public float minSpawnSpacing;
    public int requiredItems;
    
}
[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public List<LevelConfig> listLevelConfig = new List<LevelConfig>();
    
    
}
