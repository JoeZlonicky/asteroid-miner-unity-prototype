using System;
using UnityEngine;
using UnityEngine.UI;


public class SpaceBackground : MonoBehaviour
{
    [SerializeField] private Camera currentCamera;
    [SerializeField] private RingPositionCalculator ringPositionCalculator;
    [SerializeField] private float colorTransitionTimeSeconds = 1f;
    [SerializeField] private Layer[] layers;
    
    private int _currentRingIndex = -1;
    private float _colorTransitionProgress;
    
    private Color _bgColorStart;
    private Color _bgColorGoal;
    
    private Color _starColorStart;
    private Color _starColorGoal;

    private void Start()
    {
        _bgColorStart = ringPositionCalculator.CurrentRing.backgroundTint;
        _bgColorGoal = _bgColorStart;
        _starColorStart = ringPositionCalculator.CurrentRing.starTint;
        _starColorGoal = _starColorStart;
    }

    private void Update()
    {
        if (_currentRingIndex != ringPositionCalculator.CurrentRingIndex)
        {
            _currentRingIndex = ringPositionCalculator.CurrentRingIndex;
            UpdateColorGoal();
        }

        _colorTransitionProgress += Time.deltaTime;
        var currentBgColor = Color.Lerp(_bgColorStart, _bgColorGoal, _colorTransitionProgress / colorTransitionTimeSeconds);
        var currentStarColor = Color.Lerp(_starColorStart, _starColorGoal, _colorTransitionProgress / colorTransitionTimeSeconds);
        
        foreach (var layer in layers)
        {
            layer.UpdateTint(currentBgColor, currentStarColor);
            layer.UpdateOffset(currentCamera.transform.position);
        }
    }

    private void UpdateColorGoal()
    {
        var t = _colorTransitionProgress / colorTransitionTimeSeconds;
        _bgColorStart = Color.Lerp(_bgColorStart, _bgColorGoal, t);
        _starColorStart = Color.Lerp(_starColorStart, _starColorGoal, t);
        
        var newRing = ringPositionCalculator.CurrentRing;
        _colorTransitionProgress = 0f;
        _bgColorGoal = newRing.backgroundTint;
        _starColorGoal = newRing.starTint;
    }
    
    [Serializable]
    private class Layer
    {
        public RawImage starImage;
        public float parallaxStrength;
        public static readonly int PositionOffset = Shader.PropertyToID("_PositionOffset");
        private static readonly int BgColor = Shader.PropertyToID("_BGColor");
        private static readonly int StarColor = Shader.PropertyToID("_StarColor");

        public void UpdateOffset(Vector4 offset)
        {
            offset.y *= -1;
            starImage.material.SetVector(PositionOffset, offset * parallaxStrength);
        }

        public void UpdateTint(Color bgColor, Color starColor)
        {
            var backgroundAlpha = starImage.material.GetColor(BgColor).a;
            var starAlpha = starImage.material.GetColor(StarColor).a;
            
            starImage.material.SetColor(BgColor, new Color(bgColor.r, bgColor.g, bgColor.b, backgroundAlpha));
            starImage.material.SetColor(StarColor, new Color(starColor.r, starColor.g, starColor.b, starAlpha));
        }
    }
}
