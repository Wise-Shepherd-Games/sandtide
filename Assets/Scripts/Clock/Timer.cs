using Statics;
using UnityEngine;

namespace Clock
{
    public class Timer : MonoBehaviour
    {
        public static int TimePassed { get; private set; }

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
                    EventManager.OnTimerTick();
                }
            }
        }
    }
}