using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BackgroundLayer
{
    public RawImage starImage;
    public float parallaxStrength;
    private static readonly int PositionOffset = Shader.PropertyToID("_PositionOffset");

    public void UpdateOffset(Vector4 offset)
    {
        offset.y *= -1;
        starImage.material.SetVector(PositionOffset, offset * parallaxStrength);
    }
}

public class SpaceBackground : MonoBehaviour
{
    public BackgroundLayer[] layers;
    public Camera currentCamera;

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
