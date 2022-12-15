using UnityEngine.Events;

namespace Statics
{
    public static class EventManager
    {
        public static event UnityAction MakeNoise;
        public static void OnMakeNoise() => MakeNoise?.Invoke();
        
        public static event UnityAction TimerTick;
        public static void OnTimerTick() => TimerTick?.Invoke();
    }
}