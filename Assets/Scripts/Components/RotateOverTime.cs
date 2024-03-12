using System;
using UnityEngine;

namespace Components
{
    public class RotateOverTime : MonoBehaviour
    {
        public float rotationSpeedDegrees;

        private void Update()
        {
            transform.Rotate(0f, 0f, rotationSpeedDegrees * Time.deltaTime);
        }
    }
}