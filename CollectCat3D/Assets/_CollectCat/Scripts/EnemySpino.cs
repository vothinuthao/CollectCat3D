using System;
using UnityEngine;

public class EnemySpino :EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        stats = new EnemyStats("Spino", 1, 3f);

    }

    public override void Attack()
    {
      
    }

   
}
