using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : ItemBase
{
    [SerializeField] private List<ItemSO> _items;
    public CollectableItem(ItemSO data) : base(data)
    {
       
    }

    public override void UseItem()
    {
        
    }
    
}
