using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;
    private ItemSO _itemData;
    public int quantity = 1 ;
    public static event Action<string,int> OnCollect; 

    private void Start()
    {
        _playerModel = new PlayerModel();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CollectableItem"))
        {
            Debug.Log("Collected Item");
            OnCollect(_itemData.itemName, quantity);
        }
       
    }
}

