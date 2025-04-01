using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int collectableQuantity;
    private Dictionary<string, int> _inventory = new Dictionary<string, int>();
    
    public int GetCollectableQuantity() => collectableQuantity;
    public int TotalCollectableQuantity() => _inventory.Values.Sum();
    
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

        collectableQuantity = TotalCollectableQuantity();
        int total = TotalCollectableQuantity();
        Debug.Log($"Item đã thu thập: {itemName}, Số lượng hiện tại: {collectableQuantity}, Tổng số item: {total}");
        
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
        Debug.Log("InventoryController: Đăng ký event OnCollect");
    }

    private void OnDisable()
    {
        PlayerController.OnCollect -= AddItemToInventory;
        Debug.Log("InventoryController: Hủy đăng ký event OnCollect");
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
