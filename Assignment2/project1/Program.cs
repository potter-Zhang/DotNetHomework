// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Reflection;

namespace project1
{
    class PrimeFactor
    {

        static void Main(string[] args)
        {
            long num = GetLong("please enter a number: ");
            if (num == -1)
            {
                return;
            }

            List<long> primes = FindPrimes(num);
            List<long> primeFactors = DePrime(num, primes);
            PrintFactors(primeFactors);
        }
        
        static long GetLong(string prompt)
        {
            Console.WriteLine(prompt);
            long num = 0;
            try
            {
                num = long.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
            return num;
        }

        // find all the primes
        static List<long> FindPrimes(long num)
        {
            List<long> primes = new List<long>();
            primes.Add(2);
            for (long i = 3; i * i <= num; i++)
            {
                if (IsPrime(i, primes))
                    primes.Add(i);
            }
            return primes;
        }

        // determine whether a number is a prime
        static bool IsPrime(long num, List<long> primes)
        {
            foreach (long prime in primes)
            {
                if (num % prime == 0)
                {
                    return false;
                }
            }
            return true;
        }

        // find all prime factors
        static List<long> DePrime(long num, List<long> primes)
        {
            List<long> primeFactors = new List<long>();
            foreach (long prime in primes)
            {
                if (num % prime != 0)
                {
                    continue;
                }

                primeFactors.Add(prime);

                while (num % prime == 0)
                {
                    num /= prime;
                }
            }
            if (num != 1)
            {
                primeFactors.Add(num);
            }
            return primeFactors;
        }

        // print out factors
        static void PrintFactors(List<long> factors)
        {
            foreach (long factor in factors)
            {
                Console.Write(factor + " ");
            }
            Console.WriteLine("");
        }


    }

}