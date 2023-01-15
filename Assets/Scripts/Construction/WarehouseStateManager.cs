using System;
using UnityEngine;

namespace Construction
{
    public class WarehouseStateManager : MonoBehaviour
    {
        public Warehouse targetWarehouse;

        private ConstructionBaseState currentState;

        public WarehouseActiveState ActiveState = new WarehouseActiveState();
        public WarehouseInactiveState InactiveState = new WarehouseInactiveState();
        public WarehouseUpgradingState UpgradingState = new WarehouseUpgradingState();

        private void Awake()
        {
            targetWarehouse = gameObject.GetComponent<Warehouse>();
        }

        private void Start()
        {
            currentState = InactiveState;
            currentState.EnterState(this);
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void SwitchState(ConstructionBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            currentState.OnCollisionEnter2D(this, collision2D);
        }
    }
}
