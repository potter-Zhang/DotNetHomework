using System.Collections.Generic;

namespace project2
{
    class ArrayOperation
    {
        static void Main(string[] args)
        {
           PrintArrayInfo(GetListOfInt("please enter an array of ints: "));
        }

        // get an array of ints
        static int[] GetListOfInt(string prompt)
        {
            Console.WriteLine(prompt);
            string[] num = Console.ReadLine().Split();
            int[] list = new int[num.Length];

            try
            {
                for (int i = 0; i < num.Length; i++)
                {
                    list[i] = int.Parse(num[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return list;
        }

        // print out basic information of array
        static void PrintArrayInfo(int[] list)
        {
            if (list == null)
            {
                return;
            }
            Console.WriteLine("maxVal = " + list.Max());
            Console.WriteLine("minVal = " + list.Min());
            Console.WriteLine("sum = " + list.Sum());
            Console.WriteLine("mean = " + 1.0 * list.Sum() / list.Length);
        }
    }

}
