using System;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


[ExecuteInEditMode]
public class AsteroidGeneration : MonoBehaviour
{
    [SerializeField] private WorldGenData worldGen;
    [SerializeField] private bool regenerate;
    
    private void Update()
    {
        if (!regenerate) return;
        regenerate = false;

        DestroyImmediateChildren();
        
        Debug.Assert(worldGen);

        var startRadius = 0f;
        foreach (var ringData in worldGen.rings)
        {
            GenerateRing(ringData, startRadius);
            startRadius += ringData.radialWidth;
        }
    }

    private void DestroyImmediateChildren()
    {
        var childCount = transform.childCount;
        for (var i = 0; i < childCount; ++i)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void GenerateRing(WorldGenData.RingData ringData, float startRadius)
    {
        if (ringData.spawns.Length == 0) return;
        
        var nSubRings = (int)(ringData.radialWidth * ringData.radialDensity);
        var distanceBetweenSubRings = ringData.radialWidth / nSubRings;
            
        for (var i = 0; i < nSubRings; ++i)
        {
            var r = startRadius + distanceBetweenSubRings / 2f + distanceBetweenSubRings * i;
            var circumference = 2f * Math.PI * r;
            var nSpawns = (int)(circumference * ringData.centricDensity);
            var angleBetweenSpawns = 2f * Math.PI / nSpawns;

            for (var k = 0; k < nSpawns; ++k)
            {
                var spawn = ringData.spawns[Random.Range(0, ringData.spawns.Length)];
                var angle = angleBetweenSpawns * k;

                var spawnDirection = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0f);
                var pos = spawnDirection * r;

                var radialOffset = Random.Range(-ringData.radialRandomOffset, ringData.radialRandomOffset);
                pos += spawnDirection * radialOffset;

                var centricOffset = Random.Range(-ringData.centricRandomOffset, ringData.centricRandomOffset);
                pos += Vector3.Cross(spawnDirection, Vector3.forward) * centricOffset;
                    
                var position = transform.position + pos;
                var obj = Instantiate(spawn, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                obj.transform.SetParent(transform);
            }
        }
    }
}
