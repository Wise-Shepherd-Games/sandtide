using System;
using UnityEngine;

namespace Construction
{
    public class Laboratory : ConstructionBase
    {
        public LaboratoryStateManager thisStateManager;

        [Min(0)] public float transmutationBaseTime, transmutationRate, transmutationRateBonus, efficiencyBonus;

        [Min(0)] public int warehouseBonus;
        
        private void Awake()
        {
            thisStateManager = gameObject.AddComponent<LaboratoryStateManager>();
        }
    }
}
