using DG.Tweening;
using UnityEngine;

public class WaterMotion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RisingWater()
    {
        transform.DOMoveY(transform.position.y + 2, 1f);
    }

    public void FallingWater()
    {
        transform.DOMoveY(transform.position.y + 2, 1f);
    }
}
