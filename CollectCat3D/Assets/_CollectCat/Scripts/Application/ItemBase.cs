using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBase
{
    public int collectableQuantity;
    public string itemName;
    public float speed;
    public ItemSO itemData;

    public ItemBase(ItemSO data, int collectableQuantity = 0)
    {
        this.collectableQuantity = collectableQuantity;
        itemName = data.name;
        speed = data.speed;
        this.itemData = data;
    }

    public virtual void Collect(int quantity)
    {
        collectableQuantity += quantity;
        if (collectableQuantity > itemData.itemQuantity)
        {
            collectableQuantity = itemData.itemQuantity;
        }

    }

    public abstract void UseItem();

}