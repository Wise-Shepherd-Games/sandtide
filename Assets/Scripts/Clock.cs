using UnityEngine;

public class Clock : MonoBehaviour
{

    private static int seconds;
    
    public delegate void ClockUpdate();
    public static event ClockUpdate OnClockUpdate;
    
    private void OnEnable()
    {
        Timer.OnTimerTick += UpdateClock;
    }

    private void OnDisable()
    {
        Timer.OnTimerTick -= UpdateClock;
    }

    private void UpdateClock()
    {
        seconds++;
        GetCurrentClock();
        OnClockUpdate?.Invoke();
    }

    public void ResetClock()
    {
        seconds = 0;
    }

    public static int GetCurrentClock()
    {
        return seconds;
    }
}
