using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    public static class GlobVal
    {
        public static int a = 3;
        public static int b = 4;
        public static int iC = 5;
        public static int jC = 4;
        public static int topnumber = 180;
        public static int bottomnumber = 7;
        public static List<decimal> side_numbers = new List<decimal>() { 50.25M, 93.5M, 136.75M };
        public static decimal p = (decimal)((Math.Cos(Math.PI / a) + Math.Cos(Math.PI / b)) / 2);
        public static decimal w = (decimal)(2 / (1 + Math.Sqrt(1 - Math.Pow((double)p, 2))));
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
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < Values.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Values.GetLength(1) - 1; j++)
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
    public class Algorithms
    {
        // Стартові значення для нульової ітерації
        public static Matrix GetStandartMatrix(Matrix matrix)
        {
            decimal SumTopBottom = matrix.Values[0, 1] + matrix.Values[matrix.Values.GetLength(0) - 1, 1];
            int rigthIndex = matrix.Values.GetLength(1) - 1;
            for (int i = 1; i < matrix.Values.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matrix.Values.GetLength(1) - 1; j++)
                {
                    matrix.Values[i, j] = (SumTopBottom + matrix.Values[i, 0] + matrix.Values[i, rigthIndex]) / 4;
                }
            }
            return matrix;
        }
        // Ітераційний метод Лібмана
        public static Matrix LiebmanIterativeMethod(Matrix matrix)
        {
            Matrix newmatrix = matrix.Clone();
            for (int i = matrix.Values.GetLength(0) - 2; i > 0; i--)
            {
                for (int j = 1; j < matrix.Values.GetLength(1) - 1; j++)
                {
                    newmatrix.Values[i, j] = (matrix.Values[i - 1, j] + matrix.Values[i + 1, j] + matrix.Values[i, j - 1] + matrix.Values[i, j + 1]) / 4;
                }
            }
            return newmatrix;
        }
        // Ітераційний метод Гауса-Зейделя
        public static Matrix GaussSeidelIterativeMethod(Matrix matrix)
        {
            Matrix newmatrix = matrix.Clone();
            for (int i = matrix.Values.GetLength(0) - 2; i > 0; i--)
            {
                for (int j = 1; j < matrix.Values.GetLength(1) - 1; j++)
                {
                    newmatrix.Values[i, j] = (matrix.Values[i - 1, j] + newmatrix.Values[i + 1, j] + newmatrix.Values[i, j - 1] + matrix.Values[i, j + 1]) / 4;
                }
            }
            return newmatrix;
        }
        // Ітераційний метод послідовної верхньої релаксації
        public static Matrix IterativeMethodOfSuccessiveUpperRelaxation(Matrix matrix)
        {
            Matrix newmatrix = matrix.Clone();
            for (int i = matrix.Values.GetLength(0) - 2; i > 0; i--)
            {
                for (int j = 1; j < matrix.Values.GetLength(1) - 1; j++)
                {
                    newmatrix.Values[i, j] = (GlobVal.w / 4) *
                        (matrix.Values[i - 1, j] + newmatrix.Values[i + 1, j] + newmatrix.Values[i, j - 1] + matrix.Values[i, j + 1])
                        + (1 - GlobVal.w) * matrix.Values[i, j];
                }
            }
            return newmatrix;
        }
    }
    internal class Program
    {
        private static Matrix CreateStartMatrix()
        {
            Matrix matrix = new Matrix(GlobVal.iC, GlobVal.jC);
            for (int i = 0, j = GlobVal.iC - 2; i < GlobVal.side_numbers.Count; i++, j--)
            {
                matrix.Values[j, 0] = GlobVal.side_numbers[i];
                matrix.Values[j, GlobVal.jC - 1] = GlobVal.side_numbers[i];
            }
            for (int i = 1; i < GlobVal.jC - 1; i++)
            {
                matrix.Values[0, i] = GlobVal.topnumber;
                matrix.Values[GlobVal.iC - 1, i] = GlobVal.bottomnumber;
            }
            return matrix;
        }
        private static void PrintDifference(Matrix matrix1, Matrix matrix2)
        {
            decimal E = 0.1M;
            for (int i = matrix1.Values.GetLength(0) - 2; i > 0; i--)
            {
                for (int j = 1; j < matrix1.Values.GetLength(1) - 1; j++)
                {
                    decimal res = Math.Abs((matrix1.Values[i, j]) - (matrix2.Values[i, j]));
                    if (res < E)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write($"{res:F3}\t");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"p = {GlobVal.p}");
            Console.WriteLine($"w = {GlobVal.w}");
            List<Matrix> matrixs = new List<Matrix>();
            Func<Matrix, Matrix> func = Algorithms.IterativeMethodOfSuccessiveUpperRelaxation;
            Console.WriteLine("Start");
            Matrix matrix = Algorithms.GetStandartMatrix(CreateStartMatrix());
            matrixs.Add(matrix);
            Console.WriteLine(matrix);
            while (true)
            {
                Console.WriteLine($"{matrixs.Count} : ");
                Matrix newmatrix = func.Invoke(matrixs.LastOrDefault());
                PrintDifference(newmatrix, matrixs.LastOrDefault());
                Console.WriteLine(newmatrix);
                matrixs.Add(newmatrix);
                Console.ReadKey();
            }
        }
    }
}
