using System;
using UnityEngine;

namespace Components
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private GameObject followTarget;

        private void Update()
        {
            transform.position = followTarget.transform.position;
        }
    }
}
