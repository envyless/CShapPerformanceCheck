using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotECS_SpawnPrefab : MonoBehaviour
{
    public GameObject prefab;
    public int Count = 5;
    public float cubeSize = 5;


    public List<Unity.Mathematics.float3> positions = new List<Unity.Mathematics.float3>();
    public static List<Unity.Mathematics.float3> staticPositions;

    // Start is called before the first frame update
    void Start()
    {
        staticPositions = positions;

        if (prefab == null)
            return;
        for (int i = 0; i < Count; ++i)
        {
            var pos = new Vector3(Random.Range(-cubeSize, cubeSize), Random.Range(-cubeSize, cubeSize), Random.Range(-cubeSize, cubeSize));
            GameObject.Instantiate(prefab, pos, Quaternion.identity);
        }

    }
}
