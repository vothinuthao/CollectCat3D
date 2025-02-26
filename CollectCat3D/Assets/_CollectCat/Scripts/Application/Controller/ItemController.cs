using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private ItemModel _itemModel;
    [SerializeField]
    private List<ItemSO> _items;

    public void Initialize(ItemBase itemBase)
    {
        _itemModel = new ItemModel(itemBase);
    }

    public void CollectItem(int amount)
    {
        _itemModel.CollectItem(amount);
        Debug.Log($"{_itemModel.GetItemName()} collected! Current Quantity: {_itemModel.GetCollectableQuantity()}");
    }
    
}
