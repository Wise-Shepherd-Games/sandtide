using UnityEngine;
using UnityEngine.Events;

namespace EventManagers
{
	public static class NoiseEventManager
	{
		public static event UnityAction<GameObject, float> MakeNoise;
		public static void OnMakeNoise(GameObject gameObj, float noise) => MakeNoise?.Invoke(gameObj, noise);
	}
}

