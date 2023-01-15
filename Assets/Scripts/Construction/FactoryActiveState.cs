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
			var factory = factoryStateManager.targetFactory;
			factory.isExtracting = true;
			factory.UIExtractingBar.GetComponent<SpriteRenderer>().enabled = true;
			factory.UIExtractingBarContent.GetComponent<SpriteRenderer>().enabled = true;
			timePassedExtracting = Timer.GetTimePassed();
		}

		public override void UpdateState<T>(T stateManager)
		{
			var factoryStateManager = stateManager as FactoryStateManager;
			var factory = factoryStateManager.targetFactory;

			if (Input.GetKeyDown(KeyCode.Space)) factory.isExtracting = false;
			
			if (factory.isExtracting && factory.warehouseFilled < (factory.warehouseCapacity + factory.warehouseBonus))
			{
				if (timePassedExtracting != Timer.GetTimePassed())
				{
					timePassedExtracting = Timer.GetTimePassed();
					float percentageTime = (float)factory.timeExtracting / factory.timeForMaxExtracting;
					float percentageBar = percentageTime * 0.44f;
					factory.UIExtractingBarContent.transform.localScale = new Vector3(0.06f, (float)percentageBar, 1);

					if (factory.timeExtracting < factory.timeForMaxExtracting)
					{
						factory.timeExtracting++;
						factory.warehouseFilled += factory.CalculateExtraction();
						factory.EmitNoise(factory.CalculateActiveNoise());
					}
					else
					{
						factory.UIExtractingBarContent.GetComponent<SpriteRenderer>().color = Color.red;
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