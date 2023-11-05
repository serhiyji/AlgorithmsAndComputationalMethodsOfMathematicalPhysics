using System;

namespace Lab5
{
    public static class GlobVal
    {
        public static decimal l = 0.5m;
        public static decimal h = 0.1m;
        public static decimal m = 3m;
        public static decimal lambda = 0.113m;
        public static decimal c = 0.388m;
        public static decimal p = 7150m;
        public static decimal a2 = lambda / (c * p);
        public static decimal s = 0.5m;
        public static decimal a0 = a2;
        public static decimal a1 = 1 / 5;
        public static decimal r = 0.05m;
        public static decimal y = (0.5m * h) / (r * r);
        public static decimal rigthnumber = 200;
        public static decimal leftnumber = 5;
        public static decimal[] topnumbers = new decimal[] { 0m, 0.1m, 0.2m, 0.3m, 0.4m, 0.5m };
        public static int counthei = 6;
        public static decimal[] nullvalues = new decimal[GlobVal.counthei];
        public static decimal[] ai = new decimal[GlobVal.counthei];
        public static decimal[] bi = new decimal[GlobVal.counthei];
        public static decimal[] ci = new decimal[GlobVal.counthei];
        public static decimal[] Ki = new decimal[GlobVal.counthei];
    }
    internal class Program
    {
        public static void Setmass()
        {
            GlobVal.nullvalues[0] = GlobVal.leftnumber;
            GlobVal.nullvalues[GlobVal.counthei - 1] = GlobVal.rigthnumber;
            for (int i = 1; i < GlobVal.counthei - 1; i++)
            {
                GlobVal.nullvalues[i] = (((GlobVal.rigthnumber - GlobVal.leftnumber) / ((decimal)Math.Pow((double)GlobVal.l, 2)) * ((decimal)Math.Pow((double)GlobVal.topnumbers[i], 2)))) + GlobVal.leftnumber;
            }
            for (int i = 0; i < GlobVal.counthei; i++)
            {
                GlobVal.Ki[i] = GlobVal.a0 * (decimal)Math.Exp((double)((-(GlobVal.a1)) * (1m - GlobVal.topnumbers[i])));
            }
            for (int i = 1; i < GlobVal.counthei - 1; i++)
            {
                GlobVal.ai[i] = 0.5m * (GlobVal.Ki[i - 1] + GlobVal.Ki[i]);
                GlobVal.bi[i] = 0.5m * (GlobVal.Ki[i] + GlobVal.Ki[i + 1]);
                GlobVal.ci[i] = GlobVal.ai[i] + GlobVal.bi[i] + (1 / GlobVal.y);
            }
        }
        static void Print(decimal[] decimals, string title = "")
        {
            Console.Write($"{title} : ");
            for (int i = 0; i < decimals.Length; i++)
            {
                Console.Write($"{decimals[i]:F10} \t");
            }
            Console.WriteLine();
        }
        static decimal[] GetNextRow(decimal[] row)
        {
            decimal[] new_row = new decimal[row.Length];
            new_row[0] = GlobVal.leftnumber;
            new_row[row.Length - 1] = GlobVal.rigthnumber;
            decimal[] fi = new decimal[row.Length];
            for (int i = 1; i < fi.Length - 1; i++)
            {
                fi[i] = -1 * (-(1 / GlobVal.y) * row[i] - GlobVal.bi[i] * (row[i + 1] - GlobVal.bi[i]) + GlobVal.ai[i] * (row[i] - row[i - 1]));
            }
            decimal[] ai = new decimal[GlobVal.counthei - 1];
            ai[0] = 0m;
            for (int i = 1; i < ai.Length; i++)
            {
                ai[i] = GlobVal.bi[i] / (GlobVal.ci[i] - (GlobVal.ai[i] * ai[i - 1]));
            }
            //Program.Print(ai, "ai");
            decimal[] bi = new decimal[GlobVal.counthei - 1];
            bi[0] = GlobVal.leftnumber;
            for (int i = 1; i < bi.Length; i++)
            {
                bi[i] = ((GlobVal.ai[i] * bi[i - 1]) + fi[i]) / (GlobVal.ci[i] - (GlobVal.ai[i] * ai[i - 1]));
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
            Setmass();
            Program.Print(GlobVal.nullvalues, "00");
            Program.Print(GlobVal.ai, "ai");
            Program.Print(GlobVal.bi, "bi");
            Program.Print(GlobVal.ci, "ci");
            Program.Print(GlobVal.Ki, "Ki");
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