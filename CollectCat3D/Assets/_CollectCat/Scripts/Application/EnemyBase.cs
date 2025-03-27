using System;
using UnityEngine;

public class EnemyBase
{
    public string enemyName;
    public int attackPower;
    public float moveSpeed;
    public Animator _animator;
    public GameObject _enemyObj;
  
    private EnemySO _enemyData;
    protected void SetData()
    {
       
    }
    
    public void Initialize(EnemySO data)
    {
        enemyName = data.enemyName;
        attackPower = data.attack;
        moveSpeed = data.speed;
        _enemyObj = data.enemyObject;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) {
            Debug.LogError("collision.gameObject is NULL!");
            return;
        }
        if (collision.gameObject.tag=="Player")
        {
            Debug.Log(enemyName);
        }
    }
    

}
