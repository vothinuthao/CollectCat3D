using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    private ItemBase collectableItem;
    
    public ItemBase CollectableItem => collectableItem;

    public void SetData(ItemBase data)
    {
        collectableItem = data;
    }
}
