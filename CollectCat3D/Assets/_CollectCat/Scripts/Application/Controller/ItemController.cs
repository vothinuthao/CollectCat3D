using System;
using System.Collections.Generic;
using _CollectCat.Scripts.Application.Define;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ItemController : MonoBehaviour
{
    [SerializeField] private List<ItemSO> _items;
    [SerializeField] private ItemView _itemView;
    
    private List<ItemSO> _filteredItems;
    private Bounds _groundBounds;
    public int _maxSpawn;
    private int _currentSpawn = 0;
    private List<GameObject> _spawnedItems = new List<GameObject>();
    private List<ItemBase> _ListItemBase = new List<ItemBase>();
    private float minSpacing;

    public delegate void OnSpawnCountChanged(int current, int max);
    public event OnSpawnCountChanged SpawnCountChanged;

    private void Awake()
    {
        // for (int i = 0; i < _items.Count; i++)
        // {
        //     var item = _items[i];
        //     _ListItemBase.Add(new CollectableItem(item));
        // }
        
        //Debug.Log("Loaded " + _items.Count + " items");
    }

    public void Initialize()
    {
        
        if (_itemView == null)
        {
            _itemView = FindFirstObjectByType<ItemView>();
            if (_itemView == null)
            {
                Debug.LogError("ItemView is missing! Make sure it is attached.");
            }
        }
    }

    public void SetupCollectableSpawn(Bounds newBounds, int newMaxSpawn, float newMinSpacing)
    {
        _groundBounds = newBounds;
        _maxSpawn = newMaxSpawn;
        minSpacing = newMinSpacing;
        _currentSpawn = 0;
        
        // Xóa các item đã spawn trước đó (nếu có)
        ClearSpawnedItems();
        
        // Lọc danh sách item theo ID
        _filteredItems = _items.FindAll(item => item.itemType == ItemType.Collectable);
        Debug.Log("Filtered Items Count: " + _filteredItems.Count);
        
        if (_filteredItems == null || _filteredItems.Count == 0)
        {
            Debug.LogWarning("Không có item nào phù hợp với itemID: " + ItemType.Collectable);
            return;
        }
        
        UpdateSpawnCount();
        SpawnAllItems();
    }

    private void SpawnAllItems()
    {
        if (_filteredItems == null || _filteredItems.Count == 0)
        {
            Debug.LogWarning("Không thể spawn items: Danh sách item trống.");
            return;
        }
        
        for (int i = 0; i < _maxSpawn; i++)
        {
            if (!SpawnItem())
            {
                Debug.LogWarning($"Không thể spawn thêm item sau khi đã tạo {_currentSpawn}/{_maxSpawn} items.");
                break;
            }
        }
    }

    private bool SpawnItem()
    {
        // Kiểm tra điều kiện spawn
        if (_filteredItems == null || _filteredItems.Count == 0)
        {
            Debug.LogWarning("Không thể spawn item: Danh sách item trống.");
            return false;
        }
        
        if (_currentSpawn >= _maxSpawn)
        {
            Debug.LogWarning("Đã đạt giới hạn spawn tối đa.");
            return false;
        }
        
        // Chọn ngẫu nhiên một item từ danh sách đã lọc
        ItemSO itemSO = _filteredItems[Random.Range(0, _filteredItems.Count)];
        if (itemSO == null || itemSO.item == null)
        {
            Debug.LogWarning("ItemSO hoặc prefab null, không thể spawn.");
            return false;
        }
        
        // Tìm vị trí spawn hợp lệ
        Vector3 spawnPos = GetRandomSpawnPosition();
        if (spawnPos == Vector3.zero)
        {
            Debug.LogWarning("Không thể tìm vị trí spawn hợp lệ sau nhiều lần thử.");
            return false;
        }
        
        // Spawn item
        GameObject spawnedItem = Instantiate(itemSO.item, spawnPos, Quaternion.identity);
       ItemComponent itemComponent = spawnedItem.GetComponent<ItemComponent>();
       CollectableItem itemData  = new CollectableItem(itemSO);
       itemComponent.SetData(itemData);
        
        
        // Lưu item đã spawn để quản lý
        _spawnedItems.Add(spawnedItem);
        
        // Cập nhật số lượng
        _currentSpawn++;
        UpdateSpawnCount();
        
        // Áp dụng hiệu ứng spawn thông qua ItemView
        if (_itemView != null)
        {
            ApplySpawnEffect(spawnedItem, spawnPos);
        }
        
        return true;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        const int MAX_ATTEMPTS = 20; // Tăng số lần thử
        for (int attempt = 0; attempt < MAX_ATTEMPTS; attempt++)
        {
            // Sinh vị trí ngẫu nhiên trong bounds
            float randomX = Random.Range(_groundBounds.min.x, _groundBounds.max.x);
            float randomZ = Random.Range(_groundBounds.min.z, _groundBounds.max.z);
            float randomY = Random.Range(3f, 5f);
            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
            
            // Kiểm tra xem vị trí có đủ xa các vật thể khác không
            if (!Physics.CheckSphere(randomPosition, minSpacing))
            {
                return randomPosition;
            }
        }
        
        return Vector3.zero; // Không tìm thấy vị trí hợp lệ
    }


    
    private void UpdateSpawnCount()
    {
        // Thông báo thay đổi số lượng spawn thông qua event
        SpawnCountChanged?.Invoke(_currentSpawn, _maxSpawn);
        
        // Cập nhật UI thông qua ItemView
        if (_itemView != null)
        {
            _itemView.UpdateSpawnCount(_currentSpawn, _maxSpawn);
            Debug.Log($"Gọi ItemView.UpdateSpawnCount với: current={_currentSpawn}, max={_maxSpawn}");
        }
        
        if (_itemView == null) {
            Debug.LogError("_itemView là null trong UpdateSpawnCount!");
        }
    }
    
    
    private void ClearSpawnedItems()
    {
        // Xóa tất cả các item đã spawn trước đó
        foreach (GameObject item in _spawnedItems)
        {
            if (item != null)
            {
                DOTween.Kill(item.transform);
                Destroy(item);
            }
        }
        
        _spawnedItems.Clear();
        _currentSpawn = 0;
    }
    
    public void OnDestroy()
    {
        // Đảm bảo dọn dẹp tất cả các vật thể khi controller bị hủy
        ClearSpawnedItems();
        DOTween.KillAll();
    }
    
    public void ApplySpawnEffect(GameObject item, Vector3 targetPos)
    {
        item.transform.DOMoveY(targetPos.y + 0.5f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}