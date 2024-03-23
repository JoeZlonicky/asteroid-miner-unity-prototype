using ScriptableObjects;
using UnityEngine;

namespace World
{
        public class RingPositionCalculator : MonoBehaviour
        {
                [SerializeField] private WorldGenData worldGenData;
                [SerializeField] private Transform tracking;
        
                public WorldGenData.RingData CurrentRing { get; private set; }
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
                        foreach (var ringData in worldGenData.rings)
                        {
                                var boundaryRadius = currentRadius + ringData.radialWidth;
                                if (boundaryRadius < trackingRadius)
                                {
                                        currentRadius += ringData.radialWidth;
                                        continue;
                                }
                        
                                CurrentRing = ringData;
                                RingDepthRatio = (trackingRadius - currentRadius) / ringData.radialWidth;
                                return;
                        }
                }
        }
}