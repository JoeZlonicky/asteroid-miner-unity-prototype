using TMPro;
using UnityEngine;

namespace UI
{
    public class FPSLabel : MonoBehaviour
    {
        [SerializeField] private float updateInterval = 0.5f;
    
        private int _frames;
        private float _lastUpdateTime;
        private TMP_Text _text;

        private void Awake()
        {
            _lastUpdateTime = Time.time;
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            ++_frames;

            if (Time.time - _lastUpdateTime < updateInterval) return;
            
            _text.text = $"FPS: {_frames / updateInterval}";
            _frames = 0;
            _lastUpdateTime = Time.time;
        }
    }
}
