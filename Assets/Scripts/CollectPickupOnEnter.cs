using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPickupOnEnter : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var pickup = other.gameObject.GetComponent<Pickup>();
        inventory.AddItem(pickup.Data);
        pickup.Collect();
    }
}
