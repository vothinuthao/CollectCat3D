using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;

    private void Start()
    {
        _playerModel = new PlayerModel();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CollectableItem"))
        {
            Debug.Log("Collected Item");
        }
       
    }
}

