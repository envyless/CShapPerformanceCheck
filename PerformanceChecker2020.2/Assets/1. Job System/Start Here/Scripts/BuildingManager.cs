using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
namespace InfallibleCode.Start_Here
{
    public interface IConvertAble<T>
    {
        T Convert();
    }

    public static class ListToNativeArray
    {
        public static void GetNativeArray<T>(this IList convertAbleList, ref NativeArray<T> nativeArrray) where T : struct
        {
            T[] array = new T[convertAbleList.Count];
            int i = 0;
            foreach(var convertAble in convertAbleList)
            {
                var ca = convertAble as IConvertAble<T>;
                if(ca != null)
                {
                    array[i] = ca.Convert();
                }
            }
            nativeArrray = new NativeArray<T>(array, Allocator.Persistent);
        }
    }

    public class BuildingManager : MonoBehaviour
    {
        List<Building> Buildings = new List<Building>();
        //Single Job
        SingleJob Job = new SingleJob();

        // Start is called before the first frame update
        void Start()
        {
            Buildings.AddRange(GetComponentsInChildren<Building>());

            //Set Single Job
            Buildings.GetNativeArray(ref Job.BuildingDatas);
        }

        // Update is called once per frame
        void Update()
        {
            JobHandle handle = Job.Schedule();
            handle.Complete();

            //Complete이 호출되지 않으면 다른 객체의 Update는 호출 될까?                       
        }

        private void OnDestroy()
        {
            Job.Dispose();
        }

        #region SingleJob
        public struct SingleJob : IJob
        {
            public NativeArray<Building.BuildingData> BuildingDatas;


            public void Dispose()
            {
                BuildingDatas.Dispose();
            }

            public void Execute()
            {
                ulong sum = 0;
                foreach (var b in BuildingDatas)
                {
                    b.Update();
                    sum += (ulong)b.PowerUsage;
                }
                Debug.Log(sum);
            }
        }
        #endregion
    }

}