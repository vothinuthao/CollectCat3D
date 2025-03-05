using UnityEngine;

public class EnemyKentro : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        stats = new EnemyStats("Kentro", 2, 3f);

    }
    public override void Attack()
    {
       
    }
}
