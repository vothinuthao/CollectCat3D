using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected EnemyStats stats;

    protected virtual void Awake()
    {
       
    }
    

    public abstract void Attack();

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) {
            Debug.LogError("collision.gameObject is NULL!");
            return;
        }
        if (collision.gameObject.tag=="Player")
        {
            Debug.Log(stats.enemyName);
        }
    }
    

}
