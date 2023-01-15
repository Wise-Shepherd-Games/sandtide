using System;
using System.Collections.Generic;
using UnityEngine;

namespace Construction
{
	public class Factory : ConstructionBase
	{
		public FactoryStateManager thisStateManager;
	
		[Min(0)]
		public float efficiencyBonus, radiusBonus;

		[Min(0)]
		public int timeExtracting, timeForMaxExtracting, warehouseBonus;

		public bool isExtracting;

		public GameObject UIExtractingBar;
		public GameObject UIExtractingBarContent;
		
		private void Awake()
		{
			thisStateManager = gameObject.AddComponent<FactoryStateManager>();
		}

		public int CalculateExtraction()
		{
			return Convert.ToInt32(
				Math.Floor(
						Convert.ToDouble
						(
							(overallEfficiency + 
							 (overallEfficiency * efficiencyBonus)) *
							timeExtracting
						)
					)
				);
		}

		public float CalculateActiveNoise()
		{
			return activeNoise + (activeNoise * timeExtracting) - ((overallEfficiency + efficiencyBonus) * 0.05f);
		}

		public float CalculateInactiveNoise()
		{
			return inactiveNoise + (inactiveNoise * currentLevel) + (warehouseFilled * 0.025f);
		}
	}
}
