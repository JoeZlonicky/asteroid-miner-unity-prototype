using System;
using UnityEngine;
using UnityEngine.UI;


public class SpaceBackground : MonoBehaviour
{
    [Serializable]
    private struct Layer
    {
        public RawImage starImage;
        public float parallaxStrength;
        public static readonly int PositionOffset = Shader.PropertyToID("_PositionOffset");

        public void UpdateOffset(Vector4 offset)
        {
            offset.y *= -1;
            starImage.material.SetVector(PositionOffset, offset * parallaxStrength);
        }
    }
    
    [SerializeField] private Layer[] layers;
    [SerializeField] private Camera currentCamera;

    private void Awake()
    {
        Debug.Assert(currentCamera != null);
    }

    private void Update()
    {
        foreach (var layer in layers)
        {
            layer.UpdateOffset(currentCamera.transform.position);
        }
    }
}
