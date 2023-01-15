using UnityEngine;

namespace Construction
{
    public class FactoryActiveState : ConstructionBaseState
    {
        public override void EnterState<T>(T stateManager)
        {
            var factoryStateManager = stateManager as FactoryStateManager;
        }

        public override void UpdateState<T>(T stateManager)
        {
            var factoryStateManager = stateManager as FactoryStateManager;
        }

        public override void OnCollisionEnter2D<T>(T stateManager, Collision2D collision2D)
        {
            var factoryStateManager = stateManager as FactoryStateManager;
        }
    }
}
