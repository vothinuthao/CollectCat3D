using UnityEngine;

public class ItemModel 
{
    private ItemBase _itemBase;

    public ItemModel(ItemBase itemBase)
    {
        this._itemBase = itemBase;
    }

    public int GetCollectableQuantity()=> _itemBase.collectableQuantity;
    public string GetItemName() => _itemBase.itemName;

    public void CollectItem(int amount)
    {
        _itemBase.Collect(amount);
        
    }
    
}
