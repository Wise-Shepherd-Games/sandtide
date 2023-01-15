using System;
using UnityEngine;

namespace Construction
{
    public class FactoryStateManager : MonoBehaviour
    {
        public Factory targetFactory;
    
        private ConstructionBaseState currentState;
        public FactoryActiveState ActiveState;
        public FactoryInactiveState InactiveState;
        public FactoryUnloadState UnloadState;
        public FactoryUpgradingState UpgradingState;
        
        private void Awake()
        {
            ActiveState = new FactoryActiveState();
            InactiveState = new FactoryInactiveState();
            UnloadState = new FactoryUnloadState();
            UpgradingState = new FactoryUpgradingState();
            targetFactory = gameObject.GetComponent<Factory>();
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
