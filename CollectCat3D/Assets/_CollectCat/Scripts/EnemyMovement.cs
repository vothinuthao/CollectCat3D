using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float movementSpeed = 4f;

    public float turningSpeed = 3f;

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation =Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*turningSpeed);
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }
}
