using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private NavigationScript navMeshScript;
    
    private EnemyBase _enemyBase;
    

    public void SetData(EnemySO data)
    {
        var enemy = new EnemyBase()
        {
            enemyName = data.enemyName,
            moveSpeed = data.speed,
            attackPower = data.attack,
            _enemyObj = data.enemyObject,
        };
        _enemyBase = enemy;
        navMeshScript.Init(_enemyBase);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _enemyBase._animator.SetTrigger("Attack");
            
        }
    }
    
    
    
}
