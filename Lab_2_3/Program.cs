using System;

namespace Lab_2_3
{
    public static class GlobVal
    {
        public static decimal leftnumber = 5;
        public static decimal rigthnumber = 200;
        public static decimal leftnull = 0;
        public static decimal h = 0.1m;
        public static decimal rigthnull = 0.5m;
        public static int count = 6;
        public static decimal l = 0.5m;
        public static decimal a = 0.17m;
        public static decimal b = 0.17m;
        public static decimal c = 1.333333333m;
    }
    public static class RowHandler
    {
        public static decimal[] GetNullRow()
        {
            decimal[] result = new decimal[GlobVal.count];
            result[0] = GlobVal.leftnull;
            result[GlobVal.count - 1] = GlobVal.rigthnull;
            for (int i = 1; i < result.Length - 1; i++)
            {
                result[i] = GlobVal.h * i;
            }
            return result;
        }
        public static decimal[] GetFirstRow(decimal[] row)
        {
            decimal[] new_row = new decimal[GlobVal.count];
            new_row[0] = GlobVal.leftnumber;
            new_row[row.Length - 1] = GlobVal.rigthnumber;
            for (int i = 1; i < new_row.Length - 1; i++)
            {
                new_row[i] = ((new_row[row.Length - 1] - new_row[0]) / (decimal)Math.Pow((double)GlobVal.l, 2)) * (decimal)Math.Pow((double)row[i], 2) + new_row[0];
            }
            return new_row;
        }
        public static decimal[] GetNextRow1(decimal[] row)
        {
            decimal[] new_row = new decimal[row.Length];
            new_row[0] = GlobVal.leftnumber;
            new_row[row.Length - 1] = GlobVal.rigthnumber;
            for (int i = 1; i < row.Length - 1; i++)
            {
                new_row[i] = (row[i - 1] + row[i + 1] + (row[i] * 4)) / 6;
            }
            return new_row;
        }
        public static decimal[] GetNextRow2(decimal[] row)
        {
            decimal[] new_row = new decimal[row.Length];
            new_row[0] = GlobVal.leftnumber;
            new_row[row.Length - 1] = GlobVal.rigthnumber;
            decimal[] ai = new decimal[GlobVal.count - 1];
            ai[0] = 0m;
            for (int i = 1; i < ai.Length; i++)
            {
                ai[i] = GlobVal.b / (GlobVal.c - (GlobVal.a * ai[i - 1]));
            }
            //Console.WriteLine("ai {0}", string.Join(":", ai));
            decimal[] bi = new decimal[GlobVal.count - 1];
            bi[0] = GlobVal.leftnumber;
            for (int i = 1; i < bi.Length; i++)
            {
                bi[i] = ((ai[i - 1] * bi[i - 1]) + row[i]) / (GlobVal.c - (ai[i] * GlobVal.a));
            }
            //Console.WriteLine("bi {0}", string.Join(":", bi));
            for (int i = row.Length - 2; i >= 1; i--)
            {
                new_row[i] = (ai[i] * row[i + 1]) + bi[i];
            }
            return new_row;
        }
    }
    internal class Program
    {
        private static void PrintRow(decimal[] row)
        {
            foreach (decimal c in row)
            {
                Console.Write($" : {c:F3}");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            decimal[] prev_row = RowHandler.GetNullRow();
            Console.WriteLine("-0");
            PrintRow(prev_row);
            prev_row = RowHandler.GetFirstRow(prev_row);
            Console.WriteLine("0");
            PrintRow(prev_row);
            Func<decimal[], decimal[]> func = RowHandler.GetNextRow1;
            //Func<decimal[], decimal[]> func = RowHandler.GetNextRow2;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(i + 1);
                decimal[] next_row = func.Invoke(prev_row);
                PrintRow(next_row);
                prev_row = next_row;
            }
        }
    }
}
