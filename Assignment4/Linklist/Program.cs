using Assignment4;

class Test
{
    public static void Main()
    {
        GenericList<int> list = new GenericList<int>();
        for (int i = 0; i < 10; i++)
            list.Add(i);

        
        int maxVal = int.MinValue;
        int minVal = int.MaxValue;
        int sum = 0;
        list.ForEach(d => maxVal = Math.Max(maxVal, d));  
        list.ForEach(d => minVal = Math.Min(minVal, d));     
        list.ForEach(d => sum += d);
        Console.WriteLine($"max = {maxVal}, min = {minVal}, sum = {sum}");

    }
    
    
}