using System;
using UnityEngine;

namespace Components
{
    public class EnableChildrenTrigger : MonoBehaviour
    {
        private void Awake()
        {
            SetChildrenActive(false);
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            SetChildrenActive(true);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            SetChildrenActive(false);
        }

        private void SetChildrenActive(bool setActive)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(setActive);
            }
        }
    }
}