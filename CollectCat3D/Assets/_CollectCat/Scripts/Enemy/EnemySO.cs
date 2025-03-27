using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int attack;
    public float speed;
    public GameObject enemyObject;
    
}
