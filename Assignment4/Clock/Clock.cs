using System;
using System.Collections.Generic;
using System.Data;
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
    private int interval;
    private int time;
    private bool stop;
    private List<Time> alarmTime;

    public Clock(int interval, List<Time> alarmTime)
    {
        this.interval= interval;
        this.alarmTime = alarmTime;
        Tick = s => { };
        Alarm = s => { };
        stop = false;
        if (interval < 0)
            throw new ArgumentException("error: interval should be greater that zero!");
    }

    public void Start()
    {
        stop = false;
        var thread = new Thread(CheckAlarm);
        thread.Start();
        PrintTime();
        thread.Join();
        Console.WriteLine("Clock terminated");
    }

    public void PrintTime()
    {
        for (time = 0; time < interval * 60; time++)
        {
            Thread.Sleep(1000);
            Tick(this);
            Console.WriteLine($"time = {DateTime.Now.Hour}h{DateTime.Now.Minute}m{DateTime.Now.Second}s");
        }
        Stop();
    }

    public void Stop()
    {
        stop = true;
        
    }

    public void CheckAlarm()
    {
        int minute = DateTime.Now.Minute - 1;
        while (!stop)
        {
            if (minute == DateTime.Now.Minute)
                continue; 
            minute = DateTime.Now.Minute;
            foreach (Time time in alarmTime)
            {
                if (time.Hour == DateTime.Now.Hour && time.Minute == DateTime.Now.Minute)
                {
                    Alarm(this);
                    break;
                }
            }     
        }
    }

    public void AddAlarm(Time t)
    {
        alarmTime.Add(t);
    }

    public void CancelAlarm(Time t)
    {
        if (!alarmTime.Remove(t))
            throw new Exception("alarm does not exist!");
    }

}

public class Time 
{
    public Time(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public int Hour { get; set; }
    public int Minute { get; set; }
}


