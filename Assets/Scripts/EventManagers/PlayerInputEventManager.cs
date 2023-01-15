using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace EventManagers
{
	public static class PlayerInputEventManager
	{ 
		public static event UnityAction<GameObject> MouseLeftClick;
		public static void OnMouseLeftClick(GameObject gameObj) => MouseLeftClick?.Invoke(gameObj);
	}
}