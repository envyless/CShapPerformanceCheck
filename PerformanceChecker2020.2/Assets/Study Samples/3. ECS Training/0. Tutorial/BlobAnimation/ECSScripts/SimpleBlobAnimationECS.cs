using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SimpleBlobAnimationECS : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<float3> positions = new List<float3>();

    void IConvertGameObjectToEntity.Convert(
        Entity entity, 
        EntityManager dstManager, 
        GameObjectConversionSystem conversionSystem)
    {
        var blob = MoveAniBlobAsset.CreateBlobAssetReference(positions);

        // Add the generated blob asset to the blob asset store.
        // if another component generates the exact same blob asset, it will automatically be shared.
        // Ownership of the blob asset is passed to the BlobAssetStore,
        // it will automatically manage the lifetime of the blob asset.
        conversionSystem.BlobAssetStore.AddUniqueBlobAsset(ref blob);

        dstManager.AddComponentData(entity,
            new SimpleBlobAnimationComponent { moveTo = blob, index = UnityEngine.Random.Range(0, positions.Count) }); ;
    }
}


public struct SimpleBlobAnimationComponent : IComponentData
{
    // 모든 entity 는 해당 blob asset reference를 통해 moveTo 를 공유 할 수 있다!
    public BlobAssetReference<MoveAniBlobAsset> moveTo;
    public int index;
}


partial class SimpleBlobAnimationSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        uint frameCnt = (uint)UnityEngine.Time.frameCount;
        this.Entities.ForEach((
            ref Translation translation,
            ref SimpleBlobAnimationComponent component,
            ref Entity entity) => {                                
                ref var moveAni = ref component.moveTo.Value.moveAniArray;
                if (component.index >= moveAni.Length)
                {
                    component.index = 0;
                }
                var currentPos = component.moveTo.Value.moveAniArray[component.index];
                Vector3 vdir = currentPos.nextPosition - translation.Value;
                float3 dir = vdir.normalized;
                translation.Value += dir * 5 * dt;

                if(vdir.magnitude < 0.1f)
                {
                    var rand = new Unity.Mathematics.Random((uint)(entity.Index * frameCnt + 1 + component.index * frameCnt) * 0x9F6ABC1);
                    var randindex = rand.NextInt(0, moveAni.Length);
                    if (component.index == randindex)
                        component.index++;
                    else
                        component.index = randindex;
                }
                
            }).Schedule();
    }
}

public struct MoveAniBlob
{
    public float3 nextPosition;
}

public struct MoveAniBlobAsset
{
    public BlobArray<MoveAniBlob> moveAniArray;

    public static BlobAssetReference<MoveAniBlobAsset> CreateBlobAssetReference(List<float3> positions)
    {
        //Blob Builder - Blob을 만들어 주는 객체, 결국 BlobAssetReference가 필요한데...
        using (BlobBuilder blobBuilder = new BlobBuilder(Allocator.TempJob))
        {
            ref var blobAsset = ref blobBuilder.ConstructRoot<MoveAniBlobAsset>();
            var blobAssetArray = blobBuilder.Allocate(ref blobAsset.moveAniArray, positions.Count);
            for (int i = 0; i < positions.Count; ++i)
            {
                blobAssetArray[i].nextPosition = positions[i];
            }

            // 이제까지 사용한 빌더로 ref 객체를 만든다.
            var blobAssetRef = blobBuilder.CreateBlobAssetReference<MoveAniBlobAsset>(Allocator.Persistent);
            return blobAssetRef;
        }
    }
}
