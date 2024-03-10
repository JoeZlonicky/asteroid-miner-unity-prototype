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
    [SerializeField] [Min(0f)] private float maxSpawnOffset;
    [SerializeField] private bool regenerate;
    
    private void Update()
    {
        if (!regenerate) return;
        regenerate = false;
        
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        Debug.Log("Regenerating...");

        var childCount = transform.childCount;
        for (var i = 0; i < childCount; ++i)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
        Debug.Assert(worldGen);

        var startRadius = 0f;
        foreach (var ring in worldGen.rings)
        {
            if (ring.spawns.Length == 0)
            {
                startRadius += ring.radialWidth;
                continue;
            }
            
            var nSubRings = (int)(ring.radialWidth * ring.radialDensity);
            var distanceBetweenSubRings = ring.radialWidth / nSubRings;
            
            for (var i = 0; i < nSubRings; ++i)
            {
                var r = startRadius + distanceBetweenSubRings / 2f + distanceBetweenSubRings * i;
                var circumference = 2f * Math.PI * r;
                var nSpawns = (int)(circumference * ring.centricDensity);
                var angleBetweenSpawns = 2f * Math.PI / nSpawns;

                for (var k = 0; k < nSpawns; ++k)
                {
                    var spawn = ring.spawns[Random.Range(0, ring.spawns.Length - 1)];
                    var angle = angleBetweenSpawns * k;
                    var offset = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0f) * r;
                    offset += new Vector3(Random.Range(-maxSpawnOffset, maxSpawnOffset),
                        Random.Range(-maxSpawnOffset, maxSpawnOffset), 0f);
                    
                    var position = transform.position + offset;
                    var obj = Instantiate(spawn.prefab, position, Quaternion.identity);
                    obj.transform.SetParent(transform);
                }
            }
            
            startRadius += ring.radialWidth;
        }
    }
}
