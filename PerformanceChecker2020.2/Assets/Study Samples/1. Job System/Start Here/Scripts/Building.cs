using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InfallibleCode.Start_Here
{
    public class Building : MonoBehaviour, IConvertAble<Building.BuildingData>
    {
        [SerializeField] private int floors;

        private int _tenants;

        public int PowerUsage { get; private set; }

        public BuildingData Convert()
        {
            return new BuildingData(this);
        }

        private void Awake()
        {
            _tenants = floors * 300;//Random.Range(20, 500);
        }

        //private void Update()
        //{
        //    for (var i = 0; i < _tenants; i++)
        //    {
        //        PowerUsage += Random.Range(12, 24);
        //    }
        //}

        public struct BuildingData
        {
            public int _tenants;
            public int floors;
            public int PowerUsage { get; private set; }
            private Unity.Mathematics.Random _random;

            public void Update()
            {
                for (var i = 0; i < _tenants; i++)
                {
                    PowerUsage += _random.NextInt(12, 24);
                }
            }

            public BuildingData(Building origin)
            {
                _random = new Unity.Mathematics.Random(1);
                _tenants = origin._tenants;
                floors = origin.floors;
                PowerUsage = 0;                
            }
        }
    }
}