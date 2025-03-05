using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int itemID;
    public GameObject item;
    public float speed;
    public Sprite iconItem;
    public int itemQuantity;
  
}
