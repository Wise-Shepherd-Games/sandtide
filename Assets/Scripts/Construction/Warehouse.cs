using System;
using Statics;
using System.Collections.Generic;
using UnityEngine;

namespace Construction
{
    public class Warehouse : ConstructionBase
    {
       public WarehouseStateManager thisStateManager;
       
       [Min(0)]
       public float warehouseBonus, efficiencyBonus, unitsMovementVelocity, unitsMovementVelocityBonus;

       [Min(0)]
       public int intelligenceUnitsCapacity, cargoUnitsCapacity, intelligenceUnitsOnField, cargoUnitsOnField;

       private void Awake()
       {
	       thisStateManager = gameObject.AddComponent<WarehouseStateManager>();
       }

    }
}
