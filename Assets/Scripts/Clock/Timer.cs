using UnityEngine;
using EventManagers;
using Unity.VisualScripting.Dependencies.NCalc;

namespace Clock
{
    public class Timer : MonoBehaviour
    {
        private static int TimePassed { get; set; }

        [Header("Is Timer Enable?")] public bool isEnable = true;

        private readonly int timeLimit = 1;
        private float time;

        void Update()
        {
            if (isEnable)
            {
                time += Time.deltaTime;

                if (time >= timeLimit)
                {
                    time = 0;
                    TimePassed++;
                    TimeEventManager.OnTimerTick(TimePassed);
                }
            }
        }

        public static int GetTimePassed() => TimePassed;
    }
}