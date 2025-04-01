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
        // Đặt speed cho agent
        if (_agent != null && _enemyBase != null)
        {
            _agent.speed = _enemyBase.moveSpeed;
        }
    
        // Tìm player đã được spawn
        if (GameManager.Instance != null && GameManager.Instance.PlayerController != null)
        {
            GameObject spawnedPlayer = GameManager.Instance.PlayerController.GetSpawnPlayer();
            if (spawnedPlayer != null)
            {
                player = spawnedPlayer.transform;
                Debug.Log($"Enemy targeting player at {player.position}");
            }
            else
            {
                Debug.LogWarning("Player has not been spawned yet!");
            }
        }
        _enemyBase._animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        // Khởi tạo agent trong Awake
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameManager.Instance != null && GameManager.Instance.PlayerController != null)
        {
            player = GameManager.Instance.PlayerController.transform;
        }
        if (!_agent || !player) return;
    
        if (_agent.isActiveAndEnabled && _agent.isOnNavMesh)
        {
            // Kiểm tra xem điểm đến có thể đi đến được không
            NavMeshPath path = new NavMeshPath();
            if (_agent.CalculatePath(player.position, path) && path.status != NavMeshPathStatus.PathInvalid)
            {
                _agent.destination = player.position;
            }
            // Nếu không, có thể tìm điểm gần nhất trên NavMesh
            else
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(player.position, out hit, 10f, NavMesh.AllAreas))
                {
                    _agent.destination = hit.position;
                }
            }
        
            bool isMoving = _agent.velocity.magnitude > 0.1f;
            if (_enemyBase != null && _enemyBase._animator != null)
                _enemyBase._animator.SetBool("isMoving", isMoving);
        }
    }

 
}   
