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
       public float efficiencyBonus, unitsMovementVelocity, unitsMovementVelocityBonus;

       [Min(0)]
       public int warehouseBonus, intelligenceUnitsCapacity, cargoUnitsCapacity, intelligenceUnitsOnField, cargoUnitsOnField;

       private void Awake()
       {
	       thisStateManager = gameObject.AddComponent<WarehouseStateManager>();
       }

    }
}
