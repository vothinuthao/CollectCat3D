using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ItemController : MonoBehaviour
{
    private ItemModel _itemModel;
    [SerializeField]
    private List<ItemSO> _items;
    private Bounds _groundBounds;
    private int maxSpawn;
    public float minSpacing;
    private int currentSpawn = 0;

    public void Initialize(ItemBase itemBase)
    {
        _itemModel = new ItemModel(itemBase);
    }

    public void CollectItem(int amount)
    {
        _itemModel.CollectItem(amount);
        Debug.Log($"{_itemModel.GetItemName()} collected! Current Quantity: {_itemModel.GetCollectableQuantity()}");
    }

    public void SetupSpawn(Bounds newBounds, int newMaxSpawn, float newMinSpacing)
    {
        _groundBounds = newBounds;
        maxSpawn = newMaxSpawn;
        minSpacing = newMinSpacing;
        currentSpawn = 0;
        SpawnAllItem();
    }

    private void SpawnAllItem()
    {
        for (int i = 0; i < maxSpawn; i++)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if(_items.Count ==0|| currentSpawn >= maxSpawn) return;
        ItemSO itemSO = _items[Random.Range(0, _items.Count)];
        Vector3 spawnPos = GetRandomSpawnPosition();

        if (spawnPos != Vector3.zero)
        {
          GameObject spawnItem =  Instantiate(itemSO.item, spawnPos, Quaternion.identity);
            currentSpawn++;
            spawnItem.transform.DOMoveY(spawnPos.y+ 0.5f, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
        
        
        
    }
    private Vector3 GetRandomSpawnPosition()
    {
        int attempt = 10;
        while (attempt > 0)
        {
            float randomX = Random.Range(_groundBounds.min.x, _groundBounds.max.x);
            float randomZ = Random.Range(_groundBounds.min.z, _groundBounds.max.z);
            float randomY = Random.Range(3f, 4f);
            
            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
            if (!Physics.CheckSphere(randomPosition, minSpacing))
            {
                return randomPosition;
            }
            attempt--;
        }
      return Vector3.zero;
    }
    
}
