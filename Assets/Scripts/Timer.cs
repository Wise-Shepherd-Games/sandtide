using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Enable"), SerializeField] private bool isEnable;

    private readonly int timeLimit = 1;
    private float time;
    
    public delegate void TimerTick();
    public static event TimerTick OnTimerTick;
    
    void Update()
    {
        if (isEnable)
        {
            time += Time.deltaTime;
            
            if (time >= timeLimit)
            {
                time = 0;
                OnTimerTick?.Invoke();
            }
        }
    }

    public void ResetTimer()
    {
        time = 0f;
    }

    public void PauseTimer()
    {
        isEnable = !isEnable;
    }
}
