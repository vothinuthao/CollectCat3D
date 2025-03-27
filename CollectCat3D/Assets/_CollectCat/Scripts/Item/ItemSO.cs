using _CollectCat.Scripts.Application.Define;
using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public int itemID;
    public ItemType itemType;
    public string itemName;
    public GameObject item;
    public float speed;
    public Sprite iconItem;
    
  
}
