using System;
using UnityEngine;

namespace Construction
{
	public class LaboratoryStateManager : MonoBehaviour
	{
		public Laboratory targetLaboratory;

		private ConstructionBaseState currentState;

		public LaboratoryActiveState ActiveState = new LaboratoryActiveState();
		public LaboratoryInactiveState InactiveState = new LaboratoryInactiveState();
		public LaboratoryCraftingState CraftingState = new LaboratoryCraftingState();
		public LaboratoryUpgradingState UpgradingState = new LaboratoryUpgradingState();

		private void Awake()
		{
			targetLaboratory = gameObject.GetComponent<Laboratory>();
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
