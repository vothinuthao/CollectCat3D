using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemData;
    private PlayerModel _playerModel;
    [FormerlySerializedAs("collectableComponent")] [SerializeField]
    private ItemComponent itemComponent;
    public int quantity = 1 ;
    private ItemBase itemBase;
    public static event Action<ItemBase,int> OnCollect; 

    private void Start()
    {
        _playerModel = new PlayerModel();
       
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CollectableItem"))
        {

            var itemComponent = other.gameObject.GetComponent<ItemComponent>();

            if (itemComponent == null)
            {
                Debug.LogError($"❌ CollectableComponent bị thiếu trên {other.gameObject.name}!");
                return;
            }

            if (itemComponent.CollectableItem == null)
            {
                Debug.LogError($"❌ collectableItem chưa được gán trong {other.gameObject.name}!");
                return;
            }

            Debug.Log($"✅ Thu thập: {itemComponent.CollectableItem.GetItemData().itemType}");

            OnCollect(itemComponent.CollectableItem, quantity); // Đảm bảo 'quantity' có giá trị hợp lệ
            DestroyItem(other.gameObject);

            Debug.Log("🎉 Collected Item!");
        }
    }


    public void OncollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerModel.DecrementHealth(1);
        }
    }
    public void DestroyItem(GameObject gameObject)
    {
        // 🔥 Huỷ item ngay sau khi thu thập
        if ( gameObject != null)
        {
            DOTween.Kill(gameObject.transform);
            Destroy(gameObject);
          
        }
        else
        {
            Debug.LogWarning($"ItemObject bị null, không thể huỷ!");
        }
    
    }
}

