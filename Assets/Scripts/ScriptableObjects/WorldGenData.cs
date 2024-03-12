using System;
using UnityEngine;
using UnityEngine.Serialization;

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
            [Min(2)] public int nChunks;
            public float rotationSpeedDegrees;
            
            [Range(0f, 1f)] public float circumferentialDensity;
            [Range(0f, 1f)] public float radialDensity;
            
            [Min(0f)] public float circumferentialRandomOffset;
            [Min(0f)] public float radialRandomOffset;
        }
    }
}
