// See https://aka.ms/new-console-template for more information

using System;

namespace project1
{
    public class CalculatorApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("first number :");
            string num1 = Console.ReadLine();
            Console.WriteLine("second number :");
            string num2 = Console.ReadLine();
            Console.WriteLine("operator :");
            string op = Console.ReadLine();
            double op1 = 0, op2 = 0;

            try
            {
                op1 = Double.Parse(num1);
                op2 = Double.Parse(num2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            int a;
            if (op == null)
            {
                Console.WriteLine("error: missing operator");
                return;
            }
            // Console.WriteLine("");

            switch (op[0])
            {
                case '+': Console.WriteLine($"answer = {op1 + op2}"); break;
                case '-': Console.WriteLine($"answer = {op1 - op2}"); break;
                case '*': Console.WriteLine($"answer = {op1 * op2}"); break;
                case '/': Console.WriteLine($"answer = {op1 / op2}"); break;
                default: Console.WriteLine("error: unknown operator"); break;
            }



        }


    }


}




