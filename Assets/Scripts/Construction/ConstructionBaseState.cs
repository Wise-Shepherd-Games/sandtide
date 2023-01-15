using UnityEngine;

namespace Construction
{
    public abstract class ConstructionBaseState
    {
        public abstract void EnterState<T>(T stateManager);
        public abstract void UpdateState<T>(T stateManager);
        public abstract void OnCollisionEnter2D<T>(T stateManager, Collision2D collision2D);
    }
}
