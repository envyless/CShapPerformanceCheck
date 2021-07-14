using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using System;

public class CubeRotatorScene : MonoBehaviour
{
    public GameObject prefab;
    public int NumOfObject = 10;
    public bool _isUseECS = false;
    public static bool IsUseECS;

    // Start is called before the first frame update
    void Start()
    {
        int x = (int)Mathf.Sqrt(NumOfObject);
        int y = x;
        IsUseECS = _isUseECS;
        const float distance = 3;
        for (int i = 0; i < y; ++i)
        {
            for(int k = 0; k < x; ++k)
            {
                var go = GameObject.Instantiate(prefab);
                if(!IsUseECS)
                    go.AddComponent<CubeRotator>();

                go.transform.position = transform.position + new Vector3(i * distance, -k * distance, 0);
            }
        }
    }    
}

public struct PlayerTranslation
{
    public Translation translation;
    public Quaternion rotation;
    internal void Update()
    {
        //var rotation = localToWorld.rotation;
        var eulerAngles = rotation.eulerAngles;
        eulerAngles += Vector3.one * Time.deltaTime;
    }
}



[BurstCompile]
[RequireComponentTag(typeof(PlayerTag))]
public struct PlayerMoveJob : IJobParallelFor
{

    public NativeArray<PlayerTranslation> TransformArray;

    public void Execute(int index)
    {
        var data = TransformArray[index];
        data.Update();
        TransformArray[index] = data;
    }
}