using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemData;
    private PlayerModel _playerModel;
    [FormerlySerializedAs("collectableComponent")] [SerializeField]
    private ItemComponent itemComponent;
    public int quantity = 1 ;
    private ItemBase itemBase;
    [SerializeField]
    private GameObject _playerPrefab;

    private GameObject _spawnPlayer;
    private Bounds _groundBounds;
    public PlayerModel PlayerModel => _playerModel;
    public static event Action<ItemBase,int> OnCollect; 

    public void Init()
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

            OnCollect(itemComponent.CollectableItem, 1); // Đảm bảo 'quantity' có giá trị hợp lệ
            DestroyItem(other.gameObject);

            Debug.Log("🎉 Collected Item!");
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
           GameManager.Instance.DecreaseHealth(1);
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

    public void SetPosition()
    {
        if (_spawnPlayer != null)
        {
            // Kiểm tra nếu player rơi xuống dưới ngưỡng y = 5
            if (_spawnPlayer.transform.position.y < -5f)
            {
                // Gọi EndGame với tham số false (thất bại)
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.EndGame(false);
                
                    // Log thông báo
                    Debug.Log("Player rơi xuống dưới ngưỡng cho phép!");
                }
            }
        }
    }

    public void SetUpPlayerSpawn(Bounds newBounds)
    {
        _groundBounds= newBounds;
        ClearSpawnPlayer();
        if (_playerPrefab == null)
        {
            Debug.LogWarning("Item prefab null, không thể spawn.");
            return;
        }

        SpawnPlayerAtCenter();
    }
    
 
    private void SpawnPlayerAtCenter()
    {
        Vector3 centerPos = _groundBounds.center;
        centerPos.y += 0.5f;
        _spawnPlayer = Instantiate(_playerPrefab, centerPos, Quaternion.identity);
        var virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var cam in virtualCameras)
        {
            cam.Follow = _spawnPlayer.transform;
            cam.LookAt = _spawnPlayer.transform;
        }
        var navScripts = FindObjectsOfType<NavigationScript>();
        foreach (var nav in navScripts)
        {
            nav.player = _spawnPlayer.transform;
        }
        InventoryController inventoryController = GameManager.Instance.InventoryController;
        if (inventoryController != null)
        {
            inventoryController.gameObject.SetActive(false);
            inventoryController.gameObject.SetActive(true);
        }
        
        Debug.Log("Đã spawn item ở vị trí trung tâm: " + centerPos);
        CheckAndAdjustPosition();
    }

    private void CheckAndAdjustPosition()
    {
        if(_spawnPlayer == null)  return; 
        Renderer renderer = _spawnPlayer.GetComponent<Renderer>();
        if (renderer == null)
        {
            renderer = _spawnPlayer.GetComponentInChildren<Renderer>();
        }

        if (renderer != null)
        {
            RaycastHit hit;
            Vector3 rayOrigin =_spawnPlayer.transform.position;
            rayOrigin.y = _groundBounds.max.y + 5f;
            if (Physics.Raycast(rayOrigin, Vector3.down, out hit))
            {
                float playerHeight =renderer.bounds.size.y;
                Vector3 playerPosition =hit.point;
                playerPosition.y += playerHeight/2;
                _spawnPlayer.transform.position = playerPosition;
                Debug.Log("Đã điều chỉnh vị trí item: "+ playerPosition);
            }
        }
    }

    private void ClearSpawnPlayer()
    {
        if (_spawnPlayer != null)
        {
            Destroy(_spawnPlayer);
            _spawnPlayer = null;
        }
    }

    public void OnDestroy()
    {
        ClearSpawnPlayer();
    }

    public GameObject GetSpawnPlayer()
    {
        return _spawnPlayer;
    }
}

