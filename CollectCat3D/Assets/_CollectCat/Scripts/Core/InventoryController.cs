using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemBase _itemBase;
    public int collectableQuantity;
    private Dictionary<string, int> _inventory = new Dictionary<string, int>();
    public int GetCollectableQuantity()=> collectableQuantity;
    public int TotalCollectableQuantity()=> _inventory.Values.Sum();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize(ItemBase itemBase)
    {
        _itemBase = itemBase;
    }
    
    public void AddItemToInventory(string itemName ,int quantity)
    {
        if (_itemBase == null)
        {
            Debug.LogError("ItemModel chưa được khởi tạo!");
            return;
        }
        string itemname = _itemBase.GetItemName();
        if (_inventory.ContainsKey(itemname))
        {
            _inventory[itemname] += quantity;
           
        }
        else
        {
            _inventory[itemname] = quantity;
        }
        collectableQuantity = _inventory[itemname];
    }

    private void OnEnable()
    {
    PlayerController.OnCollect += AddItemToInventory;
    }

    private void OnDisable()
    {
        PlayerController.OnCollect -= AddItemToInventory;
    }
}
