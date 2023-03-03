namespace project4
{
    class Toeplitz
    {
        static void Main()
        {
            int[] dim;
            int[,] matrix = null;
            try { 
                dim = GetDimentions("input number of rows and columns:");
                matrix = GetMatrix("input matrix: ", dim[0], dim[1]);
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (matrix == null)
            {
                return;
            }
            if (IsToeplitz(matrix))
            {
                Console.WriteLine("It is a toeplitz matrix");
            }
            else
            {
                Console.WriteLine("It is not a toeplitz matrix");
            }
            
        }

        static int[] GetDimentions(string prompt)
        {
            Console.WriteLine(prompt);
            string[] list = Console.ReadLine().Split();
            int[] dim = new int[2];
            try
            {
                dim[0] = int.Parse(list[0]);
                dim[1] = int.Parse(list[1]);
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            if (dim[0] <= 0 || dim[1] <= 0)
                return null;
            return dim;
        }
        static int[,] GetMatrix(string prompt, int n, int m)
        {
            
            Console.WriteLine(prompt);
            int[,] matrix = new int[n, m];
            for (int i = 0; i < n; i++) 
            {
                string[] s = Console.ReadLine().Split();
                if (s.Length != m)
                {       
                     throw new Exception("input error");              
                }

                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = int.Parse(s[j]);
                }
            }
            return matrix;
            
        }

        static bool IsToeplitz(int[,] matrix)
        {
            
                
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            for (int i = n - 1; i >= 0; i--)
            {
                if (!diagCheck(matrix, i, 0))
                    return false;
            }

            for (int i = 0; i < m; i++)
            {
                if (!diagCheck(matrix, 0, i))
                    return false;
            }
            return true;
        }

        static bool diagCheck(int[,] matrix, int x, int y)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            int num = matrix[x, y];
            for (int i = x + 1, j = y + 1; i < n && j < m; i++, j++)
            {
                if (matrix[i, j] != num)
                    return false;
            }
            return true;
        }
    }

}
