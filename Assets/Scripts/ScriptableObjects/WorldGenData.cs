using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName="WorldGen", menuName="ScriptableObjects/WorldGen")]
    public class WorldGenData : ScriptableObject
    {
        public RingData[] rings;
    
        [Serializable]
        public struct RingData
        {
            public GameObject[] spawns;
            [Min(0f)] public float radialWidth;
            [Range(0f, 1f)] public float centricDensity;
            [Range(0f, 1f)] public float radialDensity;
            [Min(0f)] public float centricRandomOffset;
            [Min(0f)] public float radialRandomOffset;
        }
    }
}
