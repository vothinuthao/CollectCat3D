using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager:MonoBehaviour
{
    
   
    
    [SerializeField]
    private List<Transform> _enemiesTransform;
    
    [SerializeField]
    private List<EnemySO> _enemiesSO;
    
    
    private List<GameObject> _enemiesSpawned = new List<GameObject>();

    public void InitializeEnemyDate()
    {
        for (int i = 0; i < _enemiesSO.Count; i++)
        {
            GameObject prefab = _enemiesSO[i].enemyObject;
            var obj = Instantiate(prefab, _enemiesTransform[i].position, Quaternion.identity);
            _enemiesSpawned.Add(obj);
            var script = obj.GetComponent<EnemyController>();
            script.SetData(_enemiesSO[i]);
        }
    }

    public void ClearEnemies()
    {
        foreach (GameObject obj in _enemiesSpawned)
        {
            Destroy(obj);
        }
    }
    
}
