using System;
using UnityEngine;

public class LaserBlaster : MonoBehaviour
{
    [SerializeField] private Rigidbody2D laserPrefab;

    [SerializeField] [Min(0f)] private float cooldownSeconds = 0.1f;
    [SerializeField] private GameObject[] outputPoints;
    private int _currentInputPointIndex;

    private float _lastFireTimeSeconds;

    [NonSerialized] public bool Firing = false;

    private void Awake()
    {
        Debug.Assert(laserPrefab != null);
        Debug.Assert(outputPoints.Length > 0);
    }

    private void Update()
    {
        if (!Firing) return;
        if (Time.time - _lastFireTimeSeconds < cooldownSeconds) return;

        Fire();
    }

    private void Fire()
    {
        var t = outputPoints[_currentInputPointIndex].transform;
        Instantiate(laserPrefab.gameObject, t.position, t.rotation);

        _currentInputPointIndex = (_currentInputPointIndex + 1) % outputPoints.Length;
        _lastFireTimeSeconds = Time.time;
    }
}