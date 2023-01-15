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
				factory.UIExtractingBar.GetComponent<SpriteRenderer>().enabled = false;
				factory.UIExtractingBarContent.GetComponent<SpriteRenderer>().enabled = false;
				factory.UIExtractingBarContent.transform.localScale = new Vector3(0.06f, 0.01f, 1);
		}

		public override void UpdateState<T>(T stateManager)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
			var factory = factoryStateManager.targetFactory;
			
			if (factory.isSelected)
			{
				if (factory.warehouseFilled < factory.warehouseCapacity + factory.warehouseBonus)
				{
					if (Input.GetKeyDown(KeyCode.Space))
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
