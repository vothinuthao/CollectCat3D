using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    [SerializeField]
    private TextMeshProUGUI totalSpawnCountText;
  
   
   

    public void UpdateSpawnCount( int currentSpawn,int maxSpawn)
    {
        if (totalSpawnCountText != null)
        {
          totalSpawnCountText.text = $"{maxSpawn}";
          Debug.Log($"ItemView.UpdateSpawnCount nhận được: current={currentSpawn}, max={maxSpawn}");
        }
       
        if (totalSpawnCountText == null) {
            Debug.LogError("totalSpawnCountText là null!");
        }
    }
}
