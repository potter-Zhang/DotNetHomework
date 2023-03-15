using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void TickHandler(object sender);
public delegate void AlarmHandler(object sender);
public class Clock
{
    public event TickHandler Tick;
    public event AlarmHandler Alarm;
    //private int time;
    private int alarmTime;
    public void Start()
    {
        for (int time = 1; time <= 10; time++)
        {
            Thread.Sleep(1000);
            Tick(this);
            if (time == alarmTime)
                Alarm(this);
        }
    }

    public void SetAlarm(int time)
    {
        alarmTime = Math.Max(1, Math.Min(time, 10));
        
    }

}

