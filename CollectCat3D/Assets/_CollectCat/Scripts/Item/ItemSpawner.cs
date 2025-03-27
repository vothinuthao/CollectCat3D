using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private ItemController itemController;
    private ItemSO itemSO;
    // [SerializeField] private ItemSO _itemData;
    public Bounds bounds;
    public Transform ground;
    [SerializeField] private int maxItemCount = 10;
    [SerializeField] private float minSpacing = 1f;

    void Awake()
    {
        // Tìm ItemController trong GameObject hiện tại
        itemController = GetComponent<ItemController>();
        if (itemController == null)
        {
            Debug.LogError("ItemController không được tìm thấy trên GameObject này!");
        }
    }
    
    void Start()
    {
        InitializeGround();
        itemController.Initialize();
    }

    private void InitializeGround()
    {
        // Nếu ground chưa được gán, tìm trong scene
        if (ground == null)
        {
            ground = GameObject.Find("Ground")?.transform;
            if (ground == null)
            {
                Debug.LogError("Không tìm thấy GameObject với tên 'Ground'!");
                return;
            }
        }

        // Lấy bounds từ collider của ground
        Collider groundCollider = ground.GetComponent<Collider>();
        if (groundCollider != null)
        {
            bounds = groundCollider.bounds;
            Debug.Log($"Ground bounds: {bounds.min} to {bounds.max}");
        }
        else
        {
            Debug.LogError("Ground không có Collider!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
           // itemController.SetupSpawn(bounds, maxItemCount, minSpacing,);
            Debug.Log($"Đã bắt đầu spawn {maxItemCount} items với khoảng cách tối thiểu {minSpacing}");
        }
    }

    
}
