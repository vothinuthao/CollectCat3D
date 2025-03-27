using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class MapData
{
    public string mapName;
    public int mapId;
    public GameObject mapObject;
    public Bounds bounds;
    public Transform ground;
}

public class MapManager : MonoBehaviour
{
    public List<MapData> listMaps = new List<MapData>();
    public LevelData LevelData;

    private void Start()
    {
        
    }

    public void LoadMap(int level)
    {
        listMaps.ForEach(x=> x.mapObject.SetActive(x.mapId==level));
    }
   

}
