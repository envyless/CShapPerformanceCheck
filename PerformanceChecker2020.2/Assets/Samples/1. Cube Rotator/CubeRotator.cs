using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;

public class CubeRotator : MonoBehaviour
{
    public bool UseDOTS = false;
    public float Speed = 0;

    private void Start()
    {
        Speed = UnityEngine.Random.Range(30, 180);
    }

    void Update()
    {
        if(!UseDOTS)
        {
            transform.Rotate(Time.deltaTime * Speed, 0, 0);
        }
    }
}


/// <summary>
/// RotateData
/// </summary>
[GenerateAuthoringComponent]
public struct RotateData : IComponentData
{
    public float Speed;
}


class RotatorSystem : SystemBase
{
    protected override void OnUpdate()
    {        
        Entities
            .ForEach(
                (ref LocalToWorld localToWorld, in RotateData rotateData) =>
                {
                    //Debug.LogError("localToWorld.Value : "+localToWorld.Value );
                    localToWorld.Value = 0;// Matrix4x4.identity;//... // Assign localToWorld as needed for UserTransform
                }).Run();
    }
}