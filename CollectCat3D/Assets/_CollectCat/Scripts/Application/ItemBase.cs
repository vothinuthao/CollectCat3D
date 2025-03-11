using System.Collections.Generic;
using _CollectCat.Scripts.Application.Define;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBase: MonoBehaviour
{
    public int ID;
    public string itemName;
    public ItemType itemType;
    public float speed;
    public ItemSO itemData;

    protected ItemBase(ItemSO data)
    {
        itemName = data.name;
        speed = data.speed;
        this.itemData = data;
    }

    public string GetItemName() => itemName;
    public int GetItemLayer() => itemData.item.layer;

    public abstract void UseItem();

}