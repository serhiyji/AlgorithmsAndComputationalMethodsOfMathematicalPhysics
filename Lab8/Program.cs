using System;
using System.Text;

namespace Lab8
{
    public static class GlobVal
    {
        public static decimal h1h2 = 0.1m;
        public static decimal N = 5m;
        public static decimal l1a = 4m;
        public static decimal l2b = 3m;
        public static decimal k = 1m;
        public static decimal w = k * N;
        public static decimal tay = 0.001m;
        public static decimal y1y2 = tay / (h1h2 * h1h2);
    }
    public class Matrix
    {
        public decimal[,] Values { get; set; }
        public Matrix(int i, int j)
        {
            Values = new decimal[i, j];
            for (int k = 0; k < Values.GetLength(0); k++)
            {
                for (int n = 0; n < Values.GetLength(1); n++)
                {
                    Values[k, n] = new decimal(0);
                }
            }
        }
        public decimal this[int i, int j]
        {
            get { return Values[i, j]; }
        }
        public void CalculateBorders(decimal iteration)
        {
            for (int i = 0, num = 3; i < Values.GetLength(0); i++, num--)
            {
                Values[i, 0] = 4 * (num / 10m) - 12 + GlobVal.N * (decimal)Math.Sin((double)GlobVal.w * (double)iteration);
                Values[i, Values.GetLength(1) - 1] = 4 * (num / 10m) + GlobVal.k * (decimal)Math.Cos((double)GlobVal.w * (double)iteration);
            }
            for (int i = 1; i < Values.GetLength(1) - 1; i++)
            {
                Values[0, i] = 3 * (i / 10m);
                Values[Values.GetLength(0) - 1, i] = 3 * (i / 10m) - 12;
            }
        }
        public void CalculateFirstCenter()
        {
            for (int i = 1, num = Values.GetLength(0) - 2; i < Values.GetLength(0) - 1; i++, num--)
            {
                for (int j = 1; j < Values.GetLength(1) - 1; j++)
                {
                    Values[i, j] = GlobVal.N * (decimal)Math.Sin(Math.PI / ((double)(j / 10m)) / (double)GlobVal.l1a) * (decimal)Math.Cos(Math.PI * ((double)(num / 10m)) / (double)GlobVal.l2b);
                }
            }
        }
        public static Matrix GetNextmatrix(Matrix lm, decimal iteration)
        {
            Matrix matrix = lm.Clone();
            matrix.CalculateBorders(iteration);
            for (int i = 1, num = matrix.Values.GetLength(0) - 2; i < matrix.Values.GetLength(0) - 1; i++, num--)
            {
                for (int j = 1; j < matrix.Values.GetLength(1) - 1; j++)
                {
                    matrix.Values[i, j] = lm[i, j] + GlobVal.y1y2 * (lm[i, j - 1] - 2 * lm[i, j] + lm[i, j + 1]) + GlobVal.y1y2 * (lm[i + 1, j] - 2 * lm[i, j] + lm[i - 1, j]);
                }
            }
            return matrix;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Values.GetLength(0); i++)
            {
                for (int j = 0; j < Values.GetLength(1); j++)
                {
                    sb.Append($"{Values[i, j]:F3}\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public Matrix Clone()
        {
            Matrix matrix = new Matrix(this.Values.GetLength(0), this.Values.GetLength(1));
            for (int i = 0; i < Values.GetLength(0); i++)
            {
                for (int j = 0; j < Values.GetLength(1); j++)
                {
                    matrix.Values[i, j] = this.Values[i, j];
                }
            }
            return matrix;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix lastmatrix = new Matrix(4, 5);
            lastmatrix.CalculateBorders(0);
            lastmatrix.CalculateFirstCenter();
            Console.WriteLine(lastmatrix);
            for (int i = 1;i < 4;i++)
            {
                Console.WriteLine(i / 100m);
                Matrix matrix = Matrix.GetNextmatrix(lastmatrix, i / 100m);
                Console.WriteLine(matrix);
                lastmatrix = matrix;
            }
        }
    }
}