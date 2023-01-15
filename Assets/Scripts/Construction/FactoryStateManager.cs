using System;
using UnityEngine;

namespace Construction
{
    public class FactoryStateManager : MonoBehaviour
    {
        public Factory targetFactory;
    
        private ConstructionBaseState currentState;
        public FactoryActiveState ActiveState = new FactoryActiveState();
        public FactoryInactiveState InactiveState = new FactoryInactiveState();
        public FactoryUnloadState UnloadState = new FactoryUnloadState();
        public FactoryUpgradingState UpgradingState = new FactoryUpgradingState();
        
        private void Awake()
        {
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
            state.EnterState(state);
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            currentState.OnCollisionEnter2D(this, collision2D);
        }
    }
}
