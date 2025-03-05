using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
   
    private EnemyStats _stats;
    void Start()
    {
        _stats = new EnemyStats();
        _agent = GetComponent<NavMeshAgent>();
        _stats._animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = player.position;
        bool isMoving = _agent.velocity.magnitude > 0.1f;
        _stats._animator.SetBool("isMoving", isMoving);
    }
    
}   
