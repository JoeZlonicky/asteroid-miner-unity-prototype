using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupDropper : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] [Min(1)] private int nToDrop = 1;
    [SerializeField] [Min(0f)] private float dropRadius;

    private void Awake()
    {
        Debug.Assert(itemData != null);
    }

    public void Drop()
    {
        GameObject pickupPrefab = itemData.pickupPrefab;
        for (int i = 0; i < nToDrop; ++i)
        {
            Vector3 offset = dropRadius * Random.insideUnitCircle.normalized;
            Instantiate(pickupPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}
