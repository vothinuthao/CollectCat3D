using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent _agent;
   
    public EnemyBase _enemyBase;
    public void Init(EnemyBase data)
    {
        _enemyBase = data;
        player = GameManager.Instance.InventoryController.PlayerController.transform;
        _enemyBase._animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent || !player) return; 
        _agent.destination = player.position;
        bool isMoving = _agent.velocity.magnitude > 0.1f;
        _enemyBase._animator.SetBool("isMoving", isMoving);
    }
    
}   
