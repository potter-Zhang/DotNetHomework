using System.Runtime.InteropServices;

namespace project3
{
    class Eratosthenes
    {
        static bool[] vis;
        const int N = 100;
        static void Main(string[] args)
        {
            vis = new bool[N + 1];
            for (int i = 2; i <= N; i++)
            {
                if (vis[i])
                    continue;
                for (int j = i; j <= N / i; j++)
                {
                    vis[i * j] = true;
                }
            }

            for (int i = 2; i < N; i++)
            {
                if (!vis[i])
                    Console.Write(i + " ");
            }

        }
    }

}

