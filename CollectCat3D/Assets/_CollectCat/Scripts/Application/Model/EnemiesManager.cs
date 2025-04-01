using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesManager:MonoBehaviour
{
    [SerializeField]
    private List<EnemySO> _enemiesSO;
    private List<GameObject> _enemiesSpawned = new List<GameObject>();
    private Bounds _groundBounds;
    public int _maxSpawn;
    private int _currentSpawn = 0;
    private float minSpacing;

    public void SetupEnemySpawn(Bounds newBounds, int newMaxSpawn, float newMinSpacing)
    {
        _groundBounds = newBounds;
        _maxSpawn = newMaxSpawn;
        minSpacing = newMinSpacing;
        _currentSpawn = 0;
        
        // Xóa các item đã spawn trước đó (nếu có)
        ClearSpawnedItems();
        _enemiesSO = _enemiesSO.FindAll(enemy => enemy.enemyObject != null);
        if (_enemiesSO.Count == 0)
        {
            Debug.LogWarning("No valid enemy prefabs found.");
            return;
        }

        SpawnAllItems();
    }

    private void SpawnAllItems()
    {
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
        if (_enemiesSO == null || _enemiesSO.Count == 0)
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
        EnemySO enemySO = _enemiesSO[Random.Range(0, _enemiesSO.Count)];
        if (enemySO == null || enemySO.enemyObject == null)
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
        GameObject enemiesSpawned = Instantiate(enemySO.enemyObject, spawnPos, Quaternion.identity);
       var enemy = enemiesSpawned.GetComponent<EnemyController>();
       enemy.SetData(enemySO);
      
       
// Kiểm tra và cố gắng đặt enemy lên NavMesh nếu chưa ở trên
       NavMeshAgent agent = enemiesSpawned.GetComponent<NavMeshAgent>();
       if (agent != null && !agent.isOnNavMesh)
       {
           NavMeshHit hit;
           if (NavMesh.SamplePosition(spawnPos, out hit, 2.0f, NavMesh.AllAreas))
           {
               enemiesSpawned.transform.position = hit.position;
           }
           else
           {
               // Nếu không thể đặt lên NavMesh, hủy và thử lại
               Destroy(enemiesSpawned);
               return false;
           }
       }
        
        
        // Lưu item đã spawn để quản lý
        _enemiesSpawned.Add(enemiesSpawned);
        
        // Cập nhật số lượng
        _currentSpawn++;
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
    
    private void ClearSpawnedItems()
    {
        // Xóa tất cả các item đã spawn trước đó
        foreach (GameObject item in _enemiesSpawned)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        
        _enemiesSpawned.Clear();
        _currentSpawn = 0;
    }
    
    public void OnDestroy()
    {
        // Đảm bảo dọn dẹp tất cả các vật thể khi controller bị hủy
        ClearSpawnedItems();
    }
    
}
