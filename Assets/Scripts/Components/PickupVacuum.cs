using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World;

namespace Components
{
    public class PickupVacuum : MonoBehaviour
    {
        [SerializeField] private float forceAcceleration = 1f;
        [SerializeField] private float correctionForce = 2f;
        private Dictionary<Rigidbody2D, float> _affected;

        private void Awake()
        {
            _affected = new Dictionary<Rigidbody2D, float>();
        }

        private void FixedUpdate()
        {
            foreach (var rb in _affected.Keys.ToList())
            {
                _affected[rb] += forceAcceleration * Time.deltaTime;
            }
            foreach (var pair in _affected)
            {
                var targetDirection = (transform.position - pair.Key.transform.position).normalized;
                pair.Key.AddForce(pair.Value * targetDirection);

                var currentDirection = pair.Key.velocity.normalized;
                var correction = targetDirection - new Vector3(currentDirection.x, currentDirection.y);
                pair.Key.AddForce(correctionForce * correction.normalized);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (_affected.ContainsKey(rb)) return;
        
            _affected[rb] = 0f;
            other.gameObject.GetComponent<Pickup>().OnCollect += () =>
            {
                _affected.Remove(rb);
            };
        }
    }
}