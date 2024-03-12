using System;
using System.Collections.Generic;
using Components;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
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
        
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
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

        var ring = new GameObject("Ring");
        if (ringData.rotationSpeedDegrees != 0f)
        {
            var rotateOverTime = ring.AddComponent<RotateOverTime>();
            rotateOverTime.rotationSpeedDegrees = ringData.rotationSpeedDegrees;
        }
        ring.transform.SetParent(transform);

        var chunks = CreateChunks(ringData, startRadius);
        foreach (var chunk in chunks)
        {
            chunk.transform.SetParent(ring.transform);
        }
        
        var nSubRings = (int)(ringData.radialWidth * ringData.radialDensity);
        var distanceBetweenSubRings = ringData.radialWidth / nSubRings;
            
        for (var i = 0; i < nSubRings; ++i)
        {
            var r = startRadius + distanceBetweenSubRings / 2f + distanceBetweenSubRings * i;
            var circumference = 2f * Mathf.PI * r;
            var nSpawns = (int)(circumference * ringData.circumferentialDensity);
            var angleBetweenSpawns = 2f * Mathf.PI / nSpawns;

            for (var k = 0; k < nSpawns; ++k)
            {
                var spawn = ringData.spawns[Random.Range(0, ringData.spawns.Length)];
                var angle = angleBetweenSpawns * k;
                var chunkIndex = (int)(chunks.Count * (angle / (2f * Mathf.PI)));
                var chunk = chunks[Math.Clamp(chunkIndex, 0, chunks.Count)];

                var spawnDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
                var pos = spawnDirection * r;

                var radialOffset = Random.Range(-ringData.radialRandomOffset, ringData.radialRandomOffset);
                pos += spawnDirection * radialOffset;

                var centricOffset = Random.Range(-ringData.circumferentialRandomOffset, ringData.circumferentialRandomOffset);
                pos += Vector3.Cross(spawnDirection, Vector3.forward) * centricOffset;
                    
                var position = transform.position + pos;
                var obj = Instantiate(spawn, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                obj.transform.SetParent(chunk.transform);
            }
        }
    }

    private List<GameObject> CreateChunks(WorldGenData.RingData ringData, float startRadius)
    {
        var chunks = new List<GameObject>();
        var chunkAngleSpan = 2f * Mathf.PI / ringData.nChunks;
        var endRadius = startRadius + ringData.radialWidth;
        
        for (var i = 0; i < ringData.nChunks; ++i)
        {
            var chunk = new GameObject("Chunk");
            chunk.layer = LayerMask.NameToLayer("Chunk");
            var polyCollider = chunk.AddComponent<PolygonCollider2D>();
            polyCollider.isTrigger = true;
            
            var startAngle = i * chunkAngleSpan;
            var endAngle = startAngle + chunkAngleSpan;
            var halfAngle = startAngle + chunkAngleSpan / 2f;
            
            var points = new Vector2[6];
            points[0] = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle)) * startRadius;
            points[1] = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle)) * endRadius;
            points[2] = new Vector2(Mathf.Cos(halfAngle), Mathf.Sin(halfAngle)) * endRadius;
            points[3] = new Vector2(Mathf.Cos(endAngle), Mathf.Sin(endAngle)) * endRadius;
            points[4] = new Vector2(Mathf.Cos(endAngle), Mathf.Sin(endAngle)) * startRadius;
            points[5] = new Vector2(Mathf.Cos(halfAngle), Mathf.Sin(halfAngle)) * startRadius;
            polyCollider.points = points;

            chunk.AddComponent<EnableChildrenTrigger>();
            var chunkRb = chunk.AddComponent<Rigidbody2D>();
            chunkRb.bodyType = RigidbodyType2D.Kinematic;
            
            chunks.Add(chunk);
        }
        
        return chunks;
    }
}
