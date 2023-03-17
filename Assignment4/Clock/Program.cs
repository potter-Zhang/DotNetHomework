namespace Assignment4
{
    class Test
    {
        public static void Main()
        {
            List<Time> alarm = new List<Time>();
            Clock c = new Clock(1, alarm);
            alarm.Add(new Time(12, 25));
            c.Tick += Tick;
            c.Alarm += Alarm;
            c.Start();
        }

        public static void Tick(object sender)
        {
            Console.WriteLine("Tick...");
        }

        public static void Alarm(object sender)
        {
            Console.WriteLine("Alarm!");
        }
    }
}