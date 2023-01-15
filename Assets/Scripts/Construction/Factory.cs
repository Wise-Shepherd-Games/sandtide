using System;
using Statics;
using System.Collections.Generic;
using UnityEngine;

namespace Construction
{
	public class Factory : ConstructionBase
	{
		public FactoryStateManager thisStateManager;
	
		[Min(0)]
		public float efficiencyBonus, radiusBonus, warehouseBonus;

		[Min(0)]
		public int timeExtracting, timeForMaxExtracting;

		private void Awake()
		{
			thisStateManager = gameObject.AddComponent<FactoryStateManager>();
		}
	}
}
