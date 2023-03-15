namespace Assignment4
{
    class Test
    {
        public static void Main()
        {
            Clock c = new Clock();
            c.Tick += Tick;
            c.Alarm += Alarm;
            c.SetAlarm(5);
            c.Start();
        }

        public static void Tick(object sender)
        {
            Console.WriteLine("Tick");
        }

        public static void Alarm(object sender)
        {
            Console.WriteLine("Alarm!");
        }
    }
}