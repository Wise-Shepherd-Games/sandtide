using UnityEngine;
using EventManagers;

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
    }
}