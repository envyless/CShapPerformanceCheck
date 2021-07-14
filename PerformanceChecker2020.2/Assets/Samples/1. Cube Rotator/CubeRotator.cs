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
    }

    void Update()
    {

        transform.Rotate(Time.deltaTime * Speed, 0, 0);
    }
}

class RotatorSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotation rotation, in RotateData rotationSpeed) =>
        {
            rotation.Value = math.mul(rotation.Value, quaternion.RotateY(rotationSpeed.Speed));
        }).Run();

        //Entities
        //    .ForEach(
        //        (ref LocalToWorld localToWorld, in RotateData rotateData) =>
        //        {
        //            //Debug.LogError("localToWorld.Value : "+localToWorld.Value );
        //            localToWorld.Value = 0;// Matrix4x4.identity;//... // Assign localToWorld as needed for UserTransform
        //        }).Run();
    }
}
