using UnityEngine;

namespace Construction
{
    public class WarehouseInactiveState : ConstructionBaseState
    {
        public override void EnterState<T>(T stateManager)
        {
            var warehouseStateManager = stateManager as WarehouseStateManager;
        }

        public override void UpdateState<T>(T stateManager)
        {
            var warehouseStateManager = stateManager as WarehouseStateManager;
        }

        public override void OnCollisionEnter2D<T>(T stateManager, Collision2D collision2D)
        {
            var warehouseStateManager = stateManager as WarehouseStateManager;
        }
    }
}
