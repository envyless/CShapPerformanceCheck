using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class BlobSpawner : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject blobPrefab;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        // Call methods on 'dstManager' to create runtime components on 'entity' here. Remember that:
        //
        // * You can add more than one component to the entity. It's also OK to not add any at all.
        //
        // * If you want to create more than one entity from the data in this class, use the 'conversionSystem'
        //   to do it, instead of adding entities through 'dstManager' directly.
        //
        // For example,
        //   dstManager.AddComponentData(entity, new Unity.Transforms.Scale { Value = scale });

        //var spawnerData = new Spawner_FromEntity
        //{
        //    // The referenced prefab will be converted due to DeclareReferencedPrefabs.
        //    // So here we simply map the game object to an entity reference to that prefab.
        //    Prefab = conversionSystem.GetPrimaryEntity(Prefab),
        //    CountX = CountX,
        //    CountY = CountY
        //};
        //dstManager.AddComponentData(entity, spawnerData);
    }


    // Referenced prefabs have to be declared so that the conversion system knows about them ahead of time
    void IDeclareReferencedPrefabs.DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(blobPrefab);
    }
}