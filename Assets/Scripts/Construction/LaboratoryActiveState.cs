using UnityEngine;

namespace Construction
{
    public class LaboratoryActiveState : ConstructionBaseState
    {
        public override void EnterState<T>(T stateManager)
        {
            var laboratoryStateManager = stateManager as LaboratoryStateManager;
        }

        public override void UpdateState<T>(T stateManager)
        {
            var laboratoryStateManager = stateManager as LaboratoryStateManager;
        }

        public override void OnCollisionEnter2D<T>(T stateManager, Collision2D collision2D)
        {
            var laboratoryStateManager = stateManager as LaboratoryStateManager;
        }
    }
}
