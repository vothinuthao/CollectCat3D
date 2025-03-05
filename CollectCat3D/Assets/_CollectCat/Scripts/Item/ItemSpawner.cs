using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
   private ItemController itemController;
    public Bounds bounds;
    public Transform ground;
    void Start()
    {
        itemController = GetComponent<ItemController>();
        if (ground != null) {
            Collider groundCollider = ground.GetComponent<Collider>();
            if (groundCollider != null) {
                bounds = groundCollider.bounds;
            } else {
                Debug.LogError("Ground không có Collider!");
            }
        } else {
            Debug.LogError("Chưa gán Ground!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            itemController.SetupSpawn(bounds,10,1f);
        }
    }
}
