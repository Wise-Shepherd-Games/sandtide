using System;
using UnityEngine;
using Clock;

namespace Construction
{
	public class FactoryInactiveState : ConstructionBaseState
	{
		private int timePassedInactive;
		
		public override void EnterState<T>(T stateManager)
		{
				var factoryStateManager = stateManager as FactoryStateManager;
				var factory = factoryStateManager.targetFactory;
				timePassedInactive = Timer.GetTimePassed();
				factory.timeExtracting = 0;
		}

		public override void UpdateState<T>(T stateManager)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
			var factory = factoryStateManager.targetFactory;
			
			if (factory.isSelected)
			{
				if (factory.warehouseFilled < factory.warehouseCapacity + factory.warehouseBonus)
				{
					if (Input.GetKey(KeyCode.Space))
					{
						factoryStateManager.SwitchState(factoryStateManager.ActiveState);
					}
				}
			}

			if (timePassedInactive != Timer.GetTimePassed())
			{
				timePassedInactive = Timer.GetTimePassed();
				factory.EmitNoise(factory.CalculateInactiveNoise());
			}
		}

		public override void OnCollisionEnter2D<T>(T stateManager, Collision2D collision2D)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
		}
	}
}
