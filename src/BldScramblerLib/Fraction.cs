using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace BldScramblerLib
{
    public class Fraction
    {
        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public BigInteger Numerator { get; set; }
        public BigInteger Denominator { get; set; }

        public Fraction GetReciprical()
        {
            return new Fraction(Denominator, Numerator);
        }

        public static Fraction One => new Fraction(1, 1);   
        public static Fraction Zero => new Fraction(0, 1);

        public static Fraction operator +(Fraction a, Fraction b) => new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator).Simplify();

        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator).Simplify();

        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.Numerator, a.Denominator);
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public string ToPercentString()
        {
            return string.Format("{0:0.000}%", ToDouble() * 100);
        }

        public double ToDouble()
        {
            return (double)Numerator / (double)Denominator;
        }

        public static Fraction Pow(Fraction f, int power)
        {
            return new Fraction(BigInteger.Pow(f.Numerator, power), BigInteger.Pow(f.Denominator, power));
        }

        private static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            while (a % b != 0)
            {
                BigInteger c = a;
                a = b;
                b = c % a;
            }
            return b;
        }

        public Fraction Simplify()
        {
            BigInteger gcd = Gcd(Numerator, Denominator);
            return new Fraction(Numerator / gcd, Denominator / gcd);
        }
    }
}
