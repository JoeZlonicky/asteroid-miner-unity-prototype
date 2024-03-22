using System;
using UnityEngine;
using UnityEngine.UI;


public class SpaceBackground : MonoBehaviour
{
    [SerializeField] private Camera currentCamera;
    [SerializeField] private RingPositionCalculator ringPositionCalculator;
    [SerializeField] private float bgTintTransitionSpeed = 0.05f;
    [SerializeField] private float starTintTransitionSpeed = 0.5f;
    [SerializeField] private Layer[] layers;

    private void Start()
    {
        foreach (var layer in layers)
        {
            layer.starImage.material = Instantiate(layer.starImage.material);
        }
    }

    private void Update()
    {
        var currentRingData = ringPositionCalculator.CurrentRing;
        foreach (var layer in layers)
        {
            layer.UpdateBgTint(currentRingData.backgroundTint, bgTintTransitionSpeed * Time.deltaTime);
            layer.UpdateStarTint(currentRingData.starTint, starTintTransitionSpeed * Time.deltaTime);
            layer.UpdateOffset(currentCamera.transform.position);
        }
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

        public void UpdateBgTint(Color color, float maxDelta)
        {
            UpdateTint(BgColor, color, maxDelta);
        }

        public void UpdateStarTint(Color color, float maxDelta)
        {
            UpdateTint(StarColor, color, maxDelta);
        }

        private void UpdateTint(int colorParameterID, Color color, float maxDelta)
        {
            var currentColor = starImage.material.GetColor(colorParameterID);
            var goalColor = new Color(color.r, color.g, color.b, currentColor.a);
            var newColor = Vector4.MoveTowards(currentColor, goalColor, maxDelta);
            
            starImage.material.SetColor(colorParameterID, newColor);
        }
    }
}
