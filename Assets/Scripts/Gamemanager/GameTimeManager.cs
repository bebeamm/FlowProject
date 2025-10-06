using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager Instance;

    [SerializeField] private TimeOfDay timeOfDay;
    public TimeOfDay GetTimeOfDay => timeOfDay;

    [SerializeField] private int day = 1;

    public int GetDay => day;

    [SerializeField] private float timeCount = 0;

    private bool isTimePause = false;
    public void SetTimePause(bool state) => isTimePause = state;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        day = 1;
    }

    void Update()
    {
        if (isTimePause)
            return;

        timeCount += Time.deltaTime;

        if(timeCount < 180)
            timeOfDay = TimeOfDay.morning;
        else if (timeCount < 240)
            timeOfDay = TimeOfDay.evening;
        else timeOfDay = TimeOfDay.night;

        if (timeCount >= 480)
        {
            timeCount = 0;
            day++;
        }
    }

    public void NextDay()
    {
        timeOfDay = TimeOfDay.morning;
        timeCount = 0;
        day++;
    }
}

public enum TimeOfDay
{
    morning,
    evening,
    night
}
