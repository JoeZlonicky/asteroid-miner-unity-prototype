using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    private List<Rigidbody2D> _affected;

    private void Awake()
    {
        _affected = new List<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        foreach (var rb in _affected)
        {
            rb.AddForce((transform.position - rb.transform.position).normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.gameObject.GetComponent<Rigidbody2D>();
        _affected.Add(rb);
        other.gameObject.GetComponent<Pickup>().OnCollect += () =>
        {
            _affected.Remove(rb);
        };
    }
}
