using System;
using UnityEngine;
using Clock;

namespace Construction
{
	public class FactoryActiveState : ConstructionBaseState
	{
		private int timePassedExtracting;

		public override void EnterState<T>(T stateManager)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
			timePassedExtracting = Timer.GetTimePassed();
		}

		public override void UpdateState<T>(T stateManager)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
			var factory = factoryStateManager.targetFactory;

			if (Input.GetKey(KeyCode.Space) && factory.warehouseFilled < (factory.warehouseCapacity + factory.warehouseBonus))
			{
				if (timePassedExtracting != Timer.GetTimePassed())
				{
						timePassedExtracting = Timer.GetTimePassed();
						
						if (factory.timeExtracting < factory.timeForMaxExtracting)
						{
							factory.timeExtracting++;
							factory.warehouseFilled += factory.CalculateExtraction();
							factory.EmitNoise(factory.CalculateActiveNoise());
						}
						else
						{
							factory.warehouseFilled += factory.CalculateExtraction();
							factory.EmitNoise(factory.CalculateActiveNoise());
						}
				}
			}
			else
			{
				if (factory.warehouseFilled > factory.warehouseCapacity + factory.warehouseBonus)
					factory.warehouseFilled = factory.warehouseCapacity + factory.warehouseBonus;
				factoryStateManager.SwitchState(factoryStateManager.InactiveState);
			}
		}

		public override void OnCollisionEnter2D<T>(T stateManager, Collision2D collision2D)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
		}
	}
}