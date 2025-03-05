using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

   
   
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       float moveHorizontal = Input.GetAxis("Horizontal");
       float moveVertical = Input.GetAxis("Vertical");
       Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;
       bool isMoving = movement.magnitude > 0.1f;
       
    }
}
