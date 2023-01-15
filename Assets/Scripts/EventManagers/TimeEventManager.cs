using UnityEngine;
using UnityEngine.Events;

namespace EventManagers
{
	public static class TimeEventManager
	{ 
		public static event UnityAction<int> TimerTick;
		public static void OnTimerTick(int timePassed) => TimerTick?.Invoke(timePassed);
	}
}