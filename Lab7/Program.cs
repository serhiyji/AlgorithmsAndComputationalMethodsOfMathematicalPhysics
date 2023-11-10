using System;

namespace Lab7
{
    public static class GlobVal
    {
        public static decimal tay = 0.05m;
        public static decimal h = 0.1m;
        public static decimal y = (decimal)Math.Pow((double)tay, 2) / (decimal)Math.Pow((double)h, 2);
        public static decimal[] topnumbers = new decimal[] { 0m, 0.1m, 0.2m, 0.3m, 0.4m, 0.5m, 0.6m, 0.7m, 0.8m, 0.9m, 1m };
        public static int count = 11;
        public static decimal[] Fi = new decimal[count];
        public static decimal[] Psi = new decimal[count];
        public static void Setmass()
        {
            for (int i = 0; i < GlobVal.count; i++)
            {
                GlobVal.Fi[i] = 2 * GlobVal.topnumbers[i] * (GlobVal.topnumbers[i] + 1) + 0.3m;
                GlobVal.Psi[i] = 2m * (decimal)Math.Sin((double)GlobVal.topnumbers[i]);
            }
        }
    }
    internal class Program
    {
        private static decimal[,] matrix;
        static void Main(string[] args)
        {
            GlobVal.Setmass();
            decimal step = 0.05m;
            matrix = new decimal[11, 11];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = 0;
                matrix[i, matrix.GetLength(1) - 1] = 4.3m + (step * i);
            }
            for (int i = 1; i < matrix.GetLength(1) - 1; i++)
            {
                decimal koff = step;
                matrix[0, i] = 2 * GlobVal.topnumbers[i] * (GlobVal.topnumbers[i] + 1) + 0.3m;
                matrix[1, i] = matrix[0, i] + koff * GlobVal.Psi[i] + ((koff * koff) / 2) * ((GlobVal.Psi[i + 1] - 2 * GlobVal.Psi[i] + GlobVal.Psi[i - 1]) / GlobVal.topnumbers[i]);
            }
            for (int i = 2; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1) - 1; j++)
                {
                    matrix[i, j] = 2 * matrix[i - 1, j] - matrix[i - 2, j] + GlobVal.y * (matrix[i - 1, j + 1] - 2 * matrix[i - 1, j] + matrix[i - 1, j - 1]);
                }
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) ; j++)
                {
                    Console.Write($"{matrix[i, j]:F3}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
