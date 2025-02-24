using UnityEngine;

public class EnemyStats 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string enemyName;
    public int attackPower;
    public float moveSpeed;


    public EnemyStats(string name, int attack, float speed)
    {
        enemyName = name;
        attackPower = attack;
        moveSpeed = speed;
    }
}
