using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int collectableQuantity;
    [SerializeField]
    private PlayerController playerController;
    private Dictionary<string, int> _inventory = new Dictionary<string, int>();
    public PlayerController PlayerController => playerController;

    public int GetCollectableQuantity() => collectableQuantity;
    public int TotalCollectableQuantity() => _inventory.Values.Sum();

    public void Initialize()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void AddItemToInventory(ItemBase itemBase, int quantity)
    {
        string itemName = itemBase.GetItemName();
        if (_inventory.ContainsKey(itemName))
        {
            _inventory[itemName] += quantity;
        }
        else
        {
            _inventory[itemName] = quantity;
        }
        collectableQuantity = _inventory[itemName]; 

       
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelComplete();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance không tồn tại hoặc đã bị deprecated!");
        }
    }


    private void OnEnable()
    {
            PlayerController.OnCollect += AddItemToInventory;
    }

    private void OnDisable()
    {
            PlayerController.OnCollect -= AddItemToInventory;
    }

    public void ResetData()
    {
        collectableQuantity = 0;
        _inventory.Clear();
        
    }

    // public void DestroyItem()
    // {
    //     // 🔥 Huỷ item ngay sau khi thu thập
    //     if (itemObject != null)
    //     {
    //         Destroy(itemObject);
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"ItemObject bị null, không thể huỷ!");
    //     }
    //
    // }
}
