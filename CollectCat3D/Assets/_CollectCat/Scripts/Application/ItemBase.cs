using System;
using System.Collections.Generic;
using System.Linq;
using _CollectCat.Scripts.Application.Define;
using UnityEngine;

public abstract class ItemBase 
{
   
    public string ItemName { get; private set; }
    public ItemType itemType;
    public float speed;
    protected ItemSO itemData; // Đổi từ private -> protected

    protected ItemBase(ItemSO data)
    {
        if (data == null)
        {
            Debug.LogError("ItemBase constructor nhận vào một ItemSO null!");
            return;
        }

        ItemName = data.name;
        speed = data.speed;
        itemData = data;
    }

    public string GetItemName() => ItemName;
    
    public int GetItemLayer() => itemData != null ? itemData.item.layer : -1;

    public ItemSO GetItemData() => itemData;

    public abstract void UseItem();
}