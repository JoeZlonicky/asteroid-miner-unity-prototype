using System;
using ScriptableObjects;
using UnityEngine;

public class RingPositionCalculator : MonoBehaviour
{
        [SerializeField] private WorldGenData worldGenData;
        [SerializeField] private Transform tracking;
        
        public WorldGenData.RingData CurrentRing { get; private set; }
        public int CurrentRingIndex { get; private set; }
        public float RingDepthRatio { get; private set; }

        private void Awake()
        {
                CurrentRing = worldGenData.rings[0];
        }
        
        private void Update()
        {
                var trackingPosition = tracking.position;
                var trackingRadius = Mathf.Sqrt(Mathf.Pow(trackingPosition.x, 2f) + Mathf.Pow(trackingPosition.y, 2f));
                
                var currentRadius = 0f;
                var i = 0;
                foreach (var ringData in worldGenData.rings)
                {
                        var boundaryRadius = currentRadius + ringData.radialWidth;
                        if (boundaryRadius < trackingRadius)
                        {
                                currentRadius += ringData.radialWidth;
                                ++i;
                                continue;
                        }
                        
                        CurrentRing = ringData;
                        CurrentRingIndex = i;
                        RingDepthRatio = (trackingRadius - currentRadius) / ringData.radialWidth;
                        return;
                }
        }
}