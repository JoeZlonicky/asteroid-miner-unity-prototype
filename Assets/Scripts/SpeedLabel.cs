using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SpeedLabel : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] [Min(0f)] private float multiplier = 1f;
    [SerializeField] private string unit = "m/s";

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = (body.velocity.magnitude * multiplier).ToString("0") + unit;
    }
}
