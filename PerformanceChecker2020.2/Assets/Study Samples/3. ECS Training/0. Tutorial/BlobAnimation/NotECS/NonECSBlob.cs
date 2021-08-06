using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NonECSBlob : MonoBehaviour
{    
    public int index = 0;
    public int entityIndex = 0;
    public static int indexStatic = 0;
    // Start is called before the first frame update
    void Start()
    {
        entityIndex = indexStatic;
    }

    // Update is called once per frame
    void Update()
    {
        var positions = NotECS_SpawnPrefab.staticPositions;

        if (index >= positions.Count)
            index = 0;
            
        var currentPos = positions[index];
        Vector3 vdir = (Vector3)currentPos - transform.position;
        Vector3 dir = vdir.normalized;
        transform.position += dir * 5 * Time.deltaTime;

        if (vdir.magnitude < 0.1f)
        {
            var rand = new Unity.Mathematics.Random((uint)(entityIndex * Time.frameCount + 1 + index * Time.frameCount) * 0x9F6ABC1);
            var randindex = rand.NextInt(0, positions.Count);
            if (index == randindex)
                index++;
            else
                index = randindex;
        }
    }
}
