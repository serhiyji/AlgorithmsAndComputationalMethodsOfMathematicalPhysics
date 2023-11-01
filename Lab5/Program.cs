using System;

namespace Lab5
{
    public static class GlobVal
    {
        public static decimal m = 1m;
        public static decimal a0 = 10m;
        public static decimal L = 1m;
        public static decimal n = 5m;
        public static decimal h = 0.2m;
        public static decimal r = 1m;
        public static decimal t1 = r * 3m;
        public static decimal rigthnumber = (decimal)(10 * Math.Exp((double)(-(m))));
        public static decimal leftnumber = 10;
        public static decimal[] topnumbers = new decimal[] { 0m, 0.2m, 0.4m, 0.6m, 0.8m, 1m };
        public static int counthei = 6;
        public static decimal[] nullvalues = new decimal[GlobVal.counthei];
        public static decimal[] R = new decimal[GlobVal.counthei];
        public static decimal[] RPlus = new decimal[GlobVal.counthei];
        public static decimal[] RMinus = new decimal[GlobVal.counthei];
        public static decimal[] U = new decimal[GlobVal.counthei];
        public static decimal[] a = new decimal[GlobVal.counthei];
        public static decimal[] b = new decimal[GlobVal.counthei];
        public static decimal[] c = new decimal[GlobVal.counthei];
    }
    internal class Program
    {
        public static void Setmass()
        {
            for (int i = 0; i < GlobVal.counthei; i++)
            {
                GlobVal.R[i] = GlobVal.m * (decimal)Math.Cos(Math.PI * (double)(GlobVal.topnumbers[i]));
                GlobVal.RPlus[i] = 0.5m * (GlobVal.R[i] + Math.Abs(GlobVal.R[i]));
                GlobVal.RMinus[i] = 0.5m * (GlobVal.R[i] - Math.Abs(GlobVal.R[i]));
                GlobVal.U[i] = 1m / (1m + 0.5m * GlobVal.h * Math.Abs(GlobVal.R[i]));
                GlobVal.a[i] = (GlobVal.U[i] / (decimal)Math.Pow((double)GlobVal.h, 2)) - (GlobVal.RMinus[i] / GlobVal.h);
                GlobVal.b[i] = (GlobVal.U[i] / (decimal)Math.Pow((double)GlobVal.h, 2)) + (GlobVal.RPlus[i] / GlobVal.h);
                GlobVal.c[i] = ((2 * GlobVal.U[i]) / (decimal)Math.Pow((double)GlobVal.h, 2)) + (1 / GlobVal.r) + (GlobVal.RPlus[i] / GlobVal.h) - (GlobVal.RMinus[i] / GlobVal.h);
            }
            GlobVal.nullvalues[0] = GlobVal.leftnumber;
            GlobVal.nullvalues[GlobVal.counthei - 1] = GlobVal.rigthnumber;
            for (int i = 1; i < GlobVal.counthei - 1; i++)
            {
                GlobVal.nullvalues[i] = GlobVal.a0 * (decimal)Math.Exp((double)((-(GlobVal.m)) * GlobVal.topnumbers[i]));
            }
        }
        static void Print(decimal[] decimals, string title = "")
        {
            Console.Write($"{title} : ");
            for (int i = 0; i < decimals.Length; i++)
            {
                Console.Write($"{decimals[i]:F3} \t");
            }
            Console.WriteLine();
        }
        static decimal[] GetNextRow(decimal[] row) 
        {
            decimal[] new_row = new decimal[row.Length];
            new_row[0] = GlobVal.leftnumber;
            new_row[row.Length - 1] = GlobVal.rigthnumber;
            decimal[] ai = new decimal[GlobVal.counthei - 1];
            ai[0] = 0m;
            for (int i = 1; i < ai.Length; i++)
            {
                ai[i] = (GlobVal.b[i]) / (GlobVal.c[i] - GlobVal.a[i] * ai[i - 1]);
            }
            //Program.Print(ai, "ai");
            decimal[] bi = new decimal[GlobVal.counthei - 1];
            bi[0] = GlobVal.leftnumber;
            for (int i = 1; i < bi.Length; i++)
            {
                bi[i] = (GlobVal.a[i] * bi[i - 1] + (-(1m / GlobVal.r) * row[i])) / (GlobVal.c[i] - ai[i - 1] * GlobVal.a[i]);
            }
            //Program.Print(bi, "bi");
            for (int i = row.Length - 2; i >= 1; i--)
            {
                new_row[i] = (ai[i] * row[i + 1]) + bi[i];
            }
            return new_row;
        }
        static void Main(string[] args)
        {
            Program.Setmass();
            Program.Print(GlobVal.nullvalues, "00");
            Program.Print(GlobVal.R, "R");
            Program.Print(GlobVal.RPlus, "R+");
            Program.Print(GlobVal.RMinus, "R-");
            Program.Print(GlobVal.U, "U");
            Program.Print(GlobVal.a, "a");
            Program.Print(GlobVal.b, "b");
            Program.Print(GlobVal.c, "c");
            Console.WriteLine("----------------------------");
            decimal[] prev_row = GlobVal.nullvalues;
            for (int i = 0; i < 3; i++)
            {
                decimal[] next_row = GetNextRow(prev_row);
                Program.Print(next_row, $"{i + 1}");
                prev_row = next_row;
            }
        }
    }
}