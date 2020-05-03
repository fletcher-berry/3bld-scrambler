using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public static class Corners
    {
        public static Fraction ProbFloating(int numTwisted)
        {
            if (numTwisted < 0)
                throw new ArgumentException("Invalid number of corners");
            if (numTwisted == 0)
                return Fraction.One;
            return ((Fraction.One + -ProbFloating(numTwisted - 1)) * new Fraction(1, 2)).Simplify();
        }
    }
}
