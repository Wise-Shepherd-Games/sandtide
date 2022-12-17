using UnityEngine;
using UnityEngine.Events;

namespace Statics
{
    public static class EventManager
    {
        public static event UnityAction<Vector2> MakeNoise;
        public static void OnMakeNoise(Vector2 position) => MakeNoise?.Invoke(position);
        
        public static event UnityAction TimerTick;
        public static void OnTimerTick() => TimerTick?.Invoke();

        public static event UnityAction<Vector3> PlayerClick;
        public static void OnPlayerClick(Vector3 position) => PlayerClick?.Invoke(position);
    }
}