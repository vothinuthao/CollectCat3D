using UnityEngine;



public class EnemyController : MonoBehaviour
{
    private EnemyStats _stats;

    void Start()
    {
        _stats = new EnemyStats();
        _stats._animator=GetComponent<Animator>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _stats._animator.SetTrigger("Attack");
            
        }
    }

    
}
