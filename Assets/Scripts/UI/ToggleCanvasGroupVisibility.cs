using System;
using UnityEngine;

namespace UI
{
    public class ToggleCanvasGroupVisibility : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private bool _isVisible;

        private void Awake()
        {
            Debug.Assert(canvasGroup != null);
            _isVisible = canvasGroup.alpha != 0f;
        }

        public void Toggle()
        {
            if (_isVisible)
            {
                canvasGroup.alpha = 0f;
                _isVisible = false;
            }
            else
            {
                canvasGroup.alpha = 1f;
                _isVisible = true;
            }
        }
    }
}
