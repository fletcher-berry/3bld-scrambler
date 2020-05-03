using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public class MyMath
    {
        public static int Factorial(int n)
        {
            int prod = 1;
            for(int k = 2; k <= n; k++)
            {
                prod *= k;
            }
            return prod;
        }

        public static int Choose(int n, int k)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        public static Fraction Prob(int total, int selected, Fraction chance)
        {
            return (Fraction.Pow(chance, selected) * Fraction.Pow(Fraction.One + -chance, total - selected) * new Fraction(Choose(total, selected), 1)).Simplify();
        }
    }
}
